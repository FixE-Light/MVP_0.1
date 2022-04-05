using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;

    [Space(5f)]
    [Header("Login Reference")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;

    [Space(5f)]

    [Header("Register Reference")]
    [SerializeField]
    private TMP_InputField registerUserName;

    [SerializeField]
    private TMP_InputField registerEmail;

    [SerializeField]
    private TMP_InputField registerPassword;

    [SerializeField]
    private TMP_InputField registerConfirmation;

    [SerializeField]
    private TMP_Text registerOutputText;

    public static FirebaseManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

    }
    private void Start()
    {
        StartCoroutine(CheckAndFixDependencies());
    }

    private IEnumerator CheckAndFixDependencies()
    {
        var checkAndFixTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => checkAndFixTask.IsCompleted);

        var dependencyResult = checkAndFixTask.Result;

        if (dependencyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        }
        else
        {
            Debug.LogError($"Could not resolve firebase dependencies: {dependencyResult}");
        }
    }
    public void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());
        auth.StateChanged += AuthStateChanged;

        AuthStateChanged(this, null);

    }

    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();
        if (user != null)
        {
            var reloadUserTask = user.ReloadAsync();
            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);
            AutoLogin();
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void AutoLogin()
    {
        if (user != null)
        {
            // GameManager.instance.ChangeScene(1);
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Signed Out");
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log($"Signed In: {user.DisplayName}");
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }
    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUserName.text, registerEmail.text, registerPassword.text, registerConfirmation.text));
    }

    private IEnumerator LoginLogic(string _email, string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)

        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "Unknown Error, Please Try Again";

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "Please Enter Your Email";
                    Debug.Log("pipi");

                    break;

                case AuthError.MissingPassword:
                    output = "Please Enter Your Password";
                    Debug.Log("pipi");

                    break;
                case AuthError.InvalidEmail:
                    output = "Your Email bro.. check it please";
                    Debug.Log("pipi");

                    break;
                case AuthError.WrongPassword:
                    output = "Please Check Your Password";
                    break;
                case AuthError.UserNotFound:
                    output = "Account Not Found, Register Please";
                    Debug.Log("pipi");

                    break;
            }
            loginOutputText.text = output;
        }
        else
        {
            if (user.IsEmailVerified)
            {
                loginOutputText.text = "some error 1";

                Debug.Log("some error 1");

                yield return new WaitForSeconds(2f);
                GameManager.instance.ChangeScene(1);
            }
            else
            {
                loginOutputText.text = "some error 2";

                Debug.Log("some error 2");
                GameManager.instance.ChangeScene(1);
            }

        }

    }

    private IEnumerator RegisterLogic(string _username, string _email, string _password, string _confirmPassword)
    {
        if (_username == "")
        {
            registerOutputText.text = "Please Enter A Username";

        }

        else if (_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords Do Not Match";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unknown Error, Please Try Again";
                Debug.Log("kaka");

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "Check Your Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email Is Already In Use bruh";
                        break;
                    case AuthError.WeakPassword:
                        output = "Your password bro.. is so weak";
                        break;
                    case AuthError.MissingEmail:
                        output = "Please Enter Mail";
                        break;
                    case AuthError.UserNotFound:
                        output = "Gimme That Password man!";
                        Debug.Log("pipi");

                        break;

                }
                registerOutputText.text = output;
            }

            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,
                };

                var defaultUserTask = user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);

                if (defaultUserTask.Exception != null)
                {
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unknown Error, Please Try Again";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update user Canceled";
                            break;
                        case AuthError.SessionExpired:
                            output = "Session Expired";
                            break;

                    }
                    registerOutputText.text = output;
                }
                else
                {
                    Debug.Log($"Welcome To Wrath Of Balor Enjoy Your Time: {user.DisplayName} ({user.UserId}");
                }

            }
        }

    }

}

using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Application.Email;
using LangoTop.Application.Contract.Account;
using LangoTop.Domain;
using LangoTop.Interfaces;

namespace LangoTop.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IEmailService _emailService;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEncryption _encryption;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader, IAuthHelper authHelper, IEmailService emailService, IEncryption encryption,
            IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _emailService = emailService;
            _encryption = encryption;
            _roleRepository = roleRepository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            var accountBy = _accountRepository.GetBy(command.Email);

            if (_accountRepository.Exists(x => x.Email == command.Email || x.Mobile == command.Mobile))
                return operation.Failed("حساب کاربری با این مشخصات وجود دارد . می توانید وارد حساب کاربری خود شوید");


            if (accountBy?.ActiveCode != null)
                return operation.Success("کاربر گرامی کد فعال سازی حساب کاربری  به ایمیل شما ارسال شده است.");

            var password = _passwordHasher.Hash(command.Password);

            var path = $"ProfilePhotos//{command.Fullname}";
            var fileName = "";
            if (command.ProfilePhoto != null)
                fileName = _fileUploader.Upload(command.ProfilePhoto, path);
            else
                fileName = "User.png";

            var activeCode = AccountCodeGenerator.Generate();

            var account = new Account(command.Fullname, command.Username, command.Email, command.Mobile, password,
                fileName, command.Biography, command.RoleId, activeCode);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            _emailService.SendEmail(command.Email, "ثبت نام موفق!",
                $"سلام و درود به شما کاربر عزیز سایت لنگوتاپ :) \n به سایت لنگوتاپ خوش آمدید \n باعث افتخار ماست که برای یادگیری زبان انگلیسی لنگوتاپ را انتخاب کرده اید... \n جهت فعال سازی حساب کاربری خود از لینک {"https://langotop.ir/ActiveAccount/" + activeCode} وارد شوید \n باتشکر، مدیریت دیجی آجیلی");
            return operation.Success(
                "کاربر گرامی \n لینک فعالسازی حساب کربری به ایمیل شما ارسال شده است جهت فعالسازی وارد لینک ارسالی شوید و سپس وارد حساب کاربری خود شوید.  ");
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed("حساب کاربری با این اطلاعات یافت نشد");

            if (_accountRepository.Exists(x =>
                    (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed("جساب کاربری با این مشخصات وجود دارد. لطفا وارد شوید");

            var path = $"ProfilePhotos//{command.Fullname}";
            var fileName = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.Fullname, command.Username, command.Email, command.Mobile, fileName, command.Biography,
                command.RoleId,
                account.ActiveCode);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            //Hashing Password
            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult ChangePasswordByCode(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetByCode(command.Code);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordNotMatch);

            //Hashing Password
            var password = _passwordHasher.Hash(command.Password);

            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult VerifyEmail(string email)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(email);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            _emailService.SendEmail(email, "فراموشی رمز عبور",
                $"کاربر گرامی با استفاده از لینک زیر می توانید رمز عبور خود را در لنگوتاپ تغییر دهید. \n https://langotop.ir/ChangePassword/{account.ActiveCode} ");

            return operation.Success(
                "ایمیلی حاوی یک لینک به ایمیل شما ارسال شده است از طریق لینک وارد شوید و اقدام به تغییر رمز عبور خود فرمایید.");
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Email);


            if (account == null)
                return operation.Failed("حساب کاربری با این اطلاعات وجود ندارد، لطفا ثبت نام کنید.");

            var passwordResult = _passwordHasher.Check(account.Password, command.Password);

            if (!passwordResult.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            if (!account.IsActive)
                return operation.Failed(
                    "کاربر گرامی حساب کاربری شما فعال نیست، لطفا جهت فعال سازی حساب کاربری خود به لینک ارسال شده در ایمیل خود وارد شوید. ");

            var permission = _roleRepository
                .Get(account.RoleId)
                ?.Permissions
                .Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username,
                account.Mobile,
                account.Email, account.Biography, permission, account.ProfilePhoto);
            _authHelper.Signin(authViewModel);
            return operation.Success();
        }

        public OperationResult Active(string activeCode)
        {
            var operation = new OperationResult();

            var account = _accountRepository.GetByCode(activeCode);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (account.IsActive)
                return operation.Success("حساب کاربری شما فعال است");

            account.Active();
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (account.IsActive)
                return operation.Success("حساب کاربری شما فعال است");

            account.Active();
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
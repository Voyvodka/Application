using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Model.Entities.Base;
using App.Services.Extenders;

namespace App.Data.Model.SystemEntities.User;

public class UserLog : BaseEntity
{
    public string AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }

    public string Ip { get; set; }

    public string UserAgent { get; set; }

    public DateTime LoginDate { get; set; }

    public UserLogTypes UserLogType { get; set; }

    public DateTime? LogoutDate { get; set; }

    public string Extra { get; set; }

    public int DurationSecond { get; set; }

    [NotMapped]
    public string UserName { get; set; }

    [NotMapped]
    public string SessionDuration => DurationSecond.SecondToTime();
}



public enum UserLogTypes
{
    [Display(Name = "UserLogType_Register")]
    Register = 10,

    [Display(Name = "UserLogType_RegisterSocial")]
    RegisterSocial = 11,

    [Display(Name = "UserLogType_ResendActivation")]
    ResendActivation = 12,

    [Display(Name = "UserLogType_ChangeEmail")]
    ChangeEmail = 13,

    [Display(Name = "UserLogType_ChangeEmailConfirm")]
    ChangeEmailConfirm = 14,

    [Display(Name = "UserLogType_Activate")]
    Activate = 20,

    [Display(Name = "UserLogType_Login")]
    Login = 30,

    [Display(Name = "UserLogType_Login2FA")]
    Login2FA = 31,

    [Display(Name = "UserLogType_LoginRecovery")]
    LoginRecovery = 32,

    [Display(Name = "UserLogType_LoginSocial")]
    LoginSocial = 33,

    [Display(Name = "UserLogType_LoginToken")]
    LoginToken = 34,

    [Display(Name = "UserLogType_ChangePass")]
    ChangePass = 40,

    [Display(Name = "UserLogType_ChangePassReset")]
    ChangePassReset = 50,

    [Display(Name = "UserLogType_ChangePassApprove")]
    ChangePassApprove = 51,

    [Display(Name = "UserLogType_Lockout")]
    Lockout = 60,


    [Display(Name = "UserLogType_SignOut")]
    SignOut = 80,
}

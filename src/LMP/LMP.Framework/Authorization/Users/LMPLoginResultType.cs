namespace LMP.Authorization.Users
{
    public enum LMPLoginResultType
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,
        InvalidPassword,
        UserIsNotActive,

        InvalidTenancyName,
        TenantIsNotActive,
        UserEmailIsNotConfirmed
    }
}
namespace LinguaNex.Const
{
    /// <summary>
    /// 错误码
    /// 约定5位数字字符串
    /// 4XXXX：客户端错误信息
    /// 5XXXX: 服务端错误信息
    /// </summary>
    public class ErrorCode
    {
        #region 5XXXX
        public const string InternalError = "50000";
        public const string TranslateError = "54000";
        #endregion
        #region 4XXXX
        public const string Exist = "40003";
        public const string NotExist = "40004";
        public const string NotEnable = "40010";
        public const string NotSupported = "40020";
        #endregion
    }
}

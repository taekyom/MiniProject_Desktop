using NLog;
using System.Security.Cryptography;
using System.Text;
using WpfSMSApp.Model;

namespace WpfSMSApp
{
    public class Commons
    {
        //NLog 정적 인스턴스 생성
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        //로그인한 유저 정보
        public static User LOGINED_USER;

        public static string GetMd5Hash(MD5 md5Hash, string plainStr)  //모든 평문을 md5로 암호화해서 저장
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(plainStr));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
       
    }
}

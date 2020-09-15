using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreRestSqlSvr
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string BASIC = "Basic ";
        private const string AUTHORIZATION = "Authorization";
        private const string AUTHENTICATION = "WWW-Authenticate";
        private const string USER = "user"; //サンプルユーザー(本当はDBなどに格納しそこから呼び出し)
        private const string PASS = "passuser"; //サンプルパスワード

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // クライアントからのAuthorizationリクエストヘッダーを取り出し
            string ReqHead = context.Request.Headers[AUTHORIZATION];
            if (ReqHead != null && ReqHead.StartsWith(BASIC))
            {
                // クライアントからのリクエストヘッダーの頭文字Basic を取り除く(Base64のみの文字列とする)
                var Base64 = ReqHead.Substring(BASIC.Length).Trim();
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                // クライアントから送信されたパスワードはBase64文字列なのでデコードし平文文字列にコンバートする
                var PrainText = encoding.GetString(Convert.FromBase64String(Base64));


                var seperator = PrainText.IndexOf(':'); // 文字列のコロンの位置をセット
                var userid = PrainText.Substring(0, seperator); // 先頭からコロン手前まで呼出し
                var password = PrainText.Substring(seperator + 1); // コロンの次から末尾まで呼出し

                if (userid == USER && password == PASS) // 認証
                {
                    await _next.Invoke(context);
                }
                else
                {
                    // Basic認証用のリクエストヘッダーがあるがユーザー名またはパスワードが間違っている場合
                    SetResponse401Error(context);
                    return;
                }
            }
            else
            {
                // 初回アクセスなどのBasic認証用のリクエストヘッダーが無い場合
                SetResponse401Error(context);
                return;
            }
        }

        private void SetResponse401Error(HttpContext context)
        {
            // エラーコード401(Unauthorized)とレスポンスヘッダーをセットする
            context.Response.StatusCode = 401;
            context.Response.Headers.Add(AUTHENTICATION, "Basic realm=\"Original Realm\"");
        }
    }
}
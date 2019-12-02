using JWT;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeoProject.Infrastructure.Helpers
{
    public static class TokenHelper
    {
        private static string secret = "leo_lion-self$dd&#751&@#$!%%safsdfaw12314";

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="payLoad"></param>
        /// <param name="expiresMinute"></param>
        /// <param name="securityKey"></param>
        /// <returns></returns>
        public static string GenerateToken(Dictionary<string, string> payLoad, int expiresMinute=60,string securityKey="")
        {
            if (string.IsNullOrEmpty(securityKey))
            {
                securityKey = secret;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>();
            foreach (var item in payLoad.Keys)
            {
                var tempClaim = new Claim(item, payLoad[item]?.ToString());
                claims.Add(tempClaim);
            }
            //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            /**
             * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */
            var token = new JwtSecurityToken(
                //该JWT的签发者，是否使用是可选的；
                issuer: "*",
                //接收该JWT的一方，是否使用是可选的；
                audience: "*",
                claims: claims,
                notBefore: now,
                //什么时候过期，这里是一个Unix时间戳，是否使用是可选的；
                expires: now.Add(TimeSpan.FromMinutes(expiresMinute)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GetDecodeTokenString(string token, string securityKey = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("token 为空");
            }
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var payload = decoder.Decode(token, securityKey, verify: true);
            if (string.IsNullOrEmpty(payload))
            {
                throw new Exception("token 为空");
            }
            return payload;
        }
        /// <summary>
        /// 验证身份 验证签名的有效性,
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="validatePayLoad">自定义各类验证； 是否包含那种申明，或者申明的值， </param>
        /// 例如：payLoad["aud"]?.ToString() == "roberAuddience";
        /// 例如：验证是否过期 等
        /// <returns></returns>
        //public static bool Validate(string encodeJwt, Func<Dictionary<string, object>, bool> validatePayLoad)
        //{
        //    var success = true;
        //    var jwtArr = encodeJwt.Split('.');
        //    var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
        //    var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));

        //    var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(securityKey));
        //    //首先验证签名是否正确（必须的）
        //    success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
        //    if (!success)
        //    {
        //        return success;//签名不正确直接返回
        //    }
        //    //其次验证是否在有效期内（也应该必须）
        //    var now = ToUnixEpochDate(DateTime.UtcNow);
        //    success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

        //    //再其次 进行自定义的验证
        //    success = success && validatePayLoad(payLoad);

        //    return success;
        //}
    }
}

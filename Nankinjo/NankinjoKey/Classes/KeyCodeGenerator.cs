using NankinjoKey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NankinjoKey.Classes
{
    public class KeyCodeGenerator
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //パスワードに使用する文字
        private static readonly string passwordChars = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// ランダムな文字列を生成する
        /// </summary>
        /// <param name="length">生成する文字列の長さ</param>
        /// <returns>生成された文字列</returns>
        public string Generate(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                //文字の位置をランダムに選択
                int pos = r.Next(passwordChars.Length);
                //選択された位置の文字を取得
                char c = passwordChars[pos];
                //パスワードに追加
                sb.Append(c);
            }

            string keyCode = sb.ToString();

            //DBに存在したら再帰呼び出し
            if (db.KeyInfoes.Any(s => s.KeyCode == keyCode))
            {
                Generate(length);
            }

            return keyCode;
        }

    }
}
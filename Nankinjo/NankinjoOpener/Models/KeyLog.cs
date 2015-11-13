using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NankinjoOpener.Models
{
    //ログ
    public class KeyLog
    {
        public int Id { get; set; }

        //鍵情報
        [DisplayName("鍵情報")]
        [ForeignKey("KeyInfo")]
        public int KeyInfoId { get; set; }
        [DisplayName("鍵情報")]
        public virtual KeyInfo KeyInfo { get; set; }

        [DisplayName("鍵ID")]
        public string KeyId { get; set; }

        [DisplayName("使用者名")]
        public string UserName { get; set; }

        [DisplayName("鍵の状態")]
        public int Status { get; set; }

        [DisplayName("場所")]
        public string Place { get; set; }

        //レコードヘッダ
        [DisplayName("更新日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime UpdateDateTime { get; set; }

        [DisplayName("登録日")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime CreateDateTime { get; set; }

        [DisplayName("更新者")]
        public string UpdateUser { get; set; }

        [DisplayName("登録者")]
        public string CreateUser { get; set; }
    }
}
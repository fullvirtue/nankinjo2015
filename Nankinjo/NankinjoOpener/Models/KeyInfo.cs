using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NankinjoOpener.Models
{
    //鍵情報
    public class KeyInfo
    {
        public int Id { get; set; }

        [DisplayName("鍵ID")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string KeyCode { get; set; }

        [DisplayName("使用者名")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string UserName { get; set; }

        [DisplayName("鍵の状態")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public int Status { get; set; }

        [DisplayName("使用開始日")]
        [Required(ErrorMessage = "{0}は必須です。")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [DisplayName("使用終了日")]
        [Required(ErrorMessage = "{0}は必須です。")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

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
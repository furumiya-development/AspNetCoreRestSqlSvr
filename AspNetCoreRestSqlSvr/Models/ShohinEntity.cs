using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AspNetCoreRestSqlSvr.Models
{
    public class ShohinEntity
    {
        #region "フィールド"
        private int _NumId;
        private short _ShohinCode;
        private string _ShohinName;
        private decimal _EditDate;
        private decimal _EditTime;
        private string _Note;
        #endregion

        #region "定数"
        private const string INT = "int";
        private const string SMALLINT = "smallint";
        private const string CHAR = "char";
        private const string VARCHAR = "varchar";
        private const string NUMERIC = "numeric";
        #endregion

        #region "プロパティ"
        /// <summary>オートナンバー</summary>
        /// <remarks>主キー</remarks>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("NumId", Order = 0, TypeName = INT)]
        public int NumId
        {
            get { return this._NumId; }
            set { this._NumId = value; }
        }

        [Display(Name = "商品番号")]
        [Column("ShohinNum", Order = 1, TypeName = SMALLINT)]
        public short ShohinCode
        {
            get { return this._ShohinCode; }
            set { this._ShohinCode = value; }
        }

        [Display(Name = "商品名")]
        [StringLength(50)]
        [Column("ShohinName", Order = 2, TypeName = "char(50)")]
        public string ShohinName
        {
            get { return this._ShohinName; }
            set { this._ShohinName = value; }
        }

        //DisplayFormat 0=数値がある場合はその数値を無ければ0とする。 #=数値がある場合はその数値を無ければ空白とする。
        [Display(Name = "編集日付")]
        [DisplayFormat(DataFormatString = "{0:####/##/##}", ApplyFormatInEditMode = false)]
        [Required]
        [Range(0, 29991231)]
        [Column("EditDate", Order = 3, TypeName = "decimal(8, 0)")]
        public decimal EditDate
        {
            get { return this._EditDate; }
            set { this._EditDate = value; }
        }

        [Display(Name = "編集時刻")]
        [DisplayFormat(DataFormatString = "{0:0#:##:##}", ApplyFormatInEditMode = false)]
        [Required]
        [Range(0, 235959)]
        [Column("EditTime", Order = 4, TypeName = "numeric(6, 0)")]
        public decimal EditTime
        {
            get { return this._EditTime; }
            set { this._EditTime = value; }
        }

        [Display(Name = "備考")]
        [StringLength(255)]
        [Column("Note", Order = 5, TypeName = "varchar(255)")]
        public string Note
        {
            get { return this._Note; }
            set { this._Note = value; }
        }
        #endregion
    }
}
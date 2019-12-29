using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronShoes.entity
{
    public class JcTerminal
    {
        //编号
        private int termId;
        
        //终端编号（设备编号）
        private string termCode;

        //终端名称
        private string termName;

        //打码名称
        private string qrCode;

        //设备类型（:1：智能铁鞋；2：紧固器；3：人力制动器；4：防溜枕木；5：双面脱轨器；6：止轮器；）
        private int termType;

        //上道状态（1：是 0：否）
        private int inRegion;

        //充电状态（1：是 0：否）
        private int isCharge;

        //故障状态（1：是 0：否）
        private int isFault;

        //入柜状态（1：是 0：否）
        private int isIn;

        //上道区域ID:上道状态为1时有效
        private int regionId;

        //操作人ID:记录此设备当前领用人，0为无人领用
        private int operatorId;

        //删除标志（0：删除）
        private int isdelete;

        public int TermId { get => termId; set => termId = value; }
        public string TermCode { get => termCode; set => termCode = value; }
        public string TermName { get => termName; set => termName = value; }
        public string QrCode { get => qrCode; set => qrCode = value; }
        public int TermType { get => termType; set => termType = value; }
        public int InRegion { get => inRegion; set => inRegion = value; }
        public int IsCharge { get => isCharge; set => isCharge = value; }
        public int IsFault { get => isFault; set => isFault = value; }
        public int IsIn { get => isIn; set => isIn = value; }
        public int RegionId { get => regionId; set => regionId = value; }
        public int OperatorId { get => operatorId; set => operatorId = value; }
        public int Isdelete { get => isdelete; set => isdelete = value; }
    }
}

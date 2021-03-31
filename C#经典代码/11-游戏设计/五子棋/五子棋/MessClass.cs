using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 五子棋
{
    /// <summary>
    /// MessClass 的摘要说明。
    /// 发送信息的集合
    /// </summary>
    [Serializable]
    public class MessClass
    {
        /// <summary>
        ///发送方IP
        /// </summary>
        public string SIP = "";//发送方IP
        /// <summary>
        ///发送方端口号
        /// </summary>
        public string SPort = "";//发送方端口号
        /// <summary>
        ///接收方IP
        /// </summary>
        public string RIP = "";//接收方IP
        /// <summary>
        ///接收方端口号
        /// </summary>
        public string RPort = "";//接收方端口号
        /// <summary>
        ///棋子的X轴位置
        /// </summary>
        public int ChessX = 0;//棋子的X轴位置
        /// <summary>
        ///棋子的Y轴位置
        /// </summary>
        public int ChessY = 0;//棋子的Y轴位置
        /// <summary>
        ///判断对方是否下完棋
        /// </summary>
        public bool Walk = false;//判断对方是否下完棋,true为下完
        /// <summary>
        ///棋子颜色，True为黑色
        /// </summary>
        public bool Grow = true;//棋子颜色，True为黑色        
        /// <summary>
        ///黑方还是白方，True为黑主,当为黑色时，先下棋
        /// </summary>
        public bool ChessStyle = true;//棋子颜色，True为黑色
        /// <summary>
        ///发送消息类型，默认为无类型
        /// </summary>
        public SendKind sendKind = SendKind.None;//发送消息类型，默认为无类型
        /// <summary>
        ///语聊
        /// </summary>
        public string Data = "";

    }

    /// <summary>
    /// 发送类型
    /// </summary>
    [Serializable]
    public enum SendKind
    {
        None,
        /// <summary>
        ///连接
        /// </summary>
        SendConn,//连接
        /// <summary>
        ///连接成功
        /// </summary>
        SendConnHit,//连接成功
        /// <summary>
        ///开始
        /// </summary>
        SendOpen,//开始
        /// <summary>
        ///发送棋子
        /// </summary>
        SendChessman,//发送棋子
        /// <summary>
        ///发送语句
        /// </summary>
        SendMsg,//发送语句
        /// <summary>
        ///胜负
        /// </summary>
        SendNike,//胜负
        /// <summary>
        ///重新
        /// </summary>
        SendAfresh,//重新
        /// <summary>
        ///断开
        /// </summary>
        SendCut,//断开
        /// <summary>
        ///断开成功
        /// </summary>
        SendCutHit//断开成功
    }
}

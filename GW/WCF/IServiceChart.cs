﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;


namespace GW.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IServiceChart”。
    [ServiceContract]
    public interface IServiceChart
    {
        [OperationContract]
        void DoWork();

        [WebGet(UriTemplate="/GetChart", ResponseFormat=WebMessageFormat.Json)]
        [OperationContract]
        string GetChart();

        [WebGet(UriTemplate = "/GetPie", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetPie();
    }
}

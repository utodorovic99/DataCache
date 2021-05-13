﻿///////////////////////////////////////////////////////////
//  IConsumptionReq.cs
//  Implementation of the Interface IConsumptionReq
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:24 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;
using System.ServiceModel;

namespace Common_Project.DistributedServices
{
    [ServiceContract]
    public interface IConsumptionReq
    {

        /// 
        /// <param name="readReq"></param>
        [OperationContract]
        List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq);
    }//end IConsumptionReq
}
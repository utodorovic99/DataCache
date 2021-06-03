﻿///////////////////////////////////////////////////////////
//  FileControler.cs
//  Implementation of the Class FileControler
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:23 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;
using FileControler_Project.Services;
using Common_Project.Classes;
using FileControler_Project.Enums;
using FileControler_Project.Handlers.XMLHandler.Classes;
using ConnectionControler_Project.Exceptions;
using System.ServiceModel;

namespace FileControler_Project.Classes
{
    public class FileControler : IFControlerOps
    {

        private FileControlerAgent m_ConnectionControler;
        private XMLHandler m_XMLHandler;
        Dictionary<string, ELoadDataType> supportedTypes;
        private bool dbOnline;

        public FileControler()
        {
            m_XMLHandler = new XMLHandler();

            supportedTypes = new Dictionary<string, ELoadDataType>();
            supportedTypes["ostv"] = ELoadDataType.Consumption;
            m_ConnectionControler = new FileControlerAgent();
            m_ConnectionControler.TryReconnect();
        }

        ~FileControler()
        {

        }

        public bool DbOnline { get => dbOnline; }

        public bool DBTryReconnect()
        {
            
           return (dbOnline = m_ConnectionControler.TryReconnect());
        }

        /// 
        /// <param name="consumptionRecord"></param>
        private Tuple<EFileLoadStatus, ConsumptionUpdate> InitDBConsumptionWrite(List<ConsumptionRecord> consumptionRecord)
        {
            try
            {
                ConsumptionUpdate update = m_ConnectionControler.OstvConsumptionDBWrite(consumptionRecord);
                return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.Success, update);
            }
            catch (DBOfflineException)
            {
                return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.DBWriteFailed,  new ConsumptionUpdate());    
            }
            
       
        }

        /// 
        /// <param name="path"></param>
        private Tuple<EFileLoadStatus, ConsumptionUpdate> LoadOstvConsumptionStoreDB(FileInfo fileInfo)
        {
            // Add new extension handler here
            switch (fileInfo.Extension)         // Wich handler to call?
            {
                case ".xml":
                    {
                        var loaded = m_XMLHandler.XMLOstvConsumptionRead(fileInfo);
                        return this.InitDBConsumptionWrite(loaded.Item2);  // Write loaded content to distributed DB
                    }
                default:
                    {
                        return new Tuple<EFileLoadStatus, ConsumptionUpdate>(EFileLoadStatus.InvalidFileExtension, new ConsumptionUpdate());
                    }
            }
        }

        private bool IsValidDate(string year, string month, string day, ELoadDataType type)
        {
            DateTime testDateTime;
            try { testDateTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day)); }
            catch { return false; } // Not a Valid date

            switch (type)                        // //Place to add new supported types in future
            {
                case ELoadDataType.Consumption:  // ostv cannot be in future
                    {
                        if (testDateTime <= DateTime.Now) return true;
                        return false;
                    }
                case ELoadDataType.Orphan: { return true; }
            }
            return false;

        }

        /// 
        /// <param name="path"></param>
        /// <param name="dataType"></param>
        public Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>> LoadFileStoreDB(string path, ELoadDataType dataType)
        {

            FileInfo fileInfo = new FileInfo(path);
            char[] splitWordsBy = "_.".ToArray();
            string[] splitParts = fileInfo.Name.Split(splitWordsBy, StringSplitOptions.RemoveEmptyEntries);
            string timeStampBase = splitParts[1] + "-" + splitParts[2] + "-" + splitParts[3];

            if (!IsValidDate(splitParts[1], splitParts[2], splitParts[3], dataType))
                return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
                            ("", new Tuple<EFileLoadStatus, ConsumptionUpdate>
                            (EFileLoadStatus.InvalidDateTime, new ConsumptionUpdate()));


            if (!supportedTypes.ContainsKey(splitParts[0]))
                return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
                            (timeStampBase, new Tuple<EFileLoadStatus, ConsumptionUpdate>
                            (EFileLoadStatus.FileTypeNotSupported, new ConsumptionUpdate()));

            switch (dataType)
            {
                //Place to add new supported types in future
                case ELoadDataType.Consumption:
                    {
                        return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
                            (timeStampBase, LoadOstvConsumptionStoreDB(fileInfo));
                    }
            }

            return new Tuple<string, Tuple<EFileLoadStatus, ConsumptionUpdate>>
            (timeStampBase, new Tuple<EFileLoadStatus, ConsumptionUpdate>
            (EFileLoadStatus.WrongFileTypeSeleceted, new ConsumptionUpdate()));

        }

    }//end FileControler
}
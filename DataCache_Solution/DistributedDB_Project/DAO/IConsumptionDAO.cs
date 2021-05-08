///////////////////////////////////////////////////////////
//  IConsumptionDAO.cs
//  Implementation of the Interface IConsumptionDAO
//  Generated by Enterprise Architect
//  Created on:      07-May-2021 2:59:26 PM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;

public interface IConsumptionDAO:ICRUDDao<ConsumptionRecord, string>  {


	List<ConsumptionRecord> GetByCountry(string countryID);

	List<ConsumptionRecord> GetByCountryAndDate(string countryID, string targetTimestamp);

	List<ConsumptionRecord> GetByCountryAndDatespan(string gID, string fromTimestamp, string tillTimestamp);


	List<ConsumptionRecord> GetByGeographyAndAfterDate(string gID, string from);


	List<ConsumptionRecord> GetByGeographyAndBeforeDate(string targetGID, string targetTimestamp);

	ConsumptionUpdate StoreConsumption(ConsumptionRecord consumptionRecords);
	ConsumptionUpdate StoreConsumption(List<ConsumptionRecord> consumptionRecords);

}//end IConsumptionDAO
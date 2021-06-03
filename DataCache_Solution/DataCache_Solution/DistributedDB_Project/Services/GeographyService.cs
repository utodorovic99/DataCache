///////////////////////////////////////////////////////////
//  GeographyService.cs
//  Implementation of the Class GeographyService
//  Generated by Enterprise Architect
//  Created on:      07-May-2021 2:59:26 PM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;
using DistributedDB_Project.Exceptions.ExceptionAbstraction;

public class GeographyService {

	private IGeographyDAO m_IGeographyDAO;

	public GeographyService(){
		m_IGeographyDAO = new GeographyDaoImpl();
	}

	~GeographyService(){

	}

	public Dictionary<string, string> HandleShowAll(){

		var loadedGeos = m_IGeographyDAO.FindAll();

		// Client side has less resources use faster server to
		// covert it into dictionary to make things faster
		// on client side
		Dictionary<string, string> retVal = new Dictionary<string, string>();
		foreach(var loadedGeo in loadedGeos)
        {
			retVal.Add(loadedGeo.GName, loadedGeo.GID); // Reverse Table key is not dictionary key 
		}												// because client only knows name not ID, faster search
		return retVal;
	}

	public GeoRecord HandleShowByGID(string key)
	{
		return m_IGeographyDAO.FindById(key);
	}

	public List<GeoRecord> HandleShowMultipleByGID(List<string> keys)
	{
		return m_IGeographyDAO.FindAllById(keys) as List<GeoRecord>;
	}

	/// 
	/// <param name="id"></param>
	/// <param name="name"></param>
	public bool HandleSignleGeoWrite(GeoRecord toAddGeo){

		try { m_IGeographyDAO.Save(toAddGeo);			 return true; }
		catch (PrimaryKeyConstraintViolationException) { return false; }
 
	}

	public void HandleMultipleGeoWrite(List<GeoRecord> toAddGeo)
	{
		m_IGeographyDAO.SaveAll(toAddGeo);
	}

	/// 
	/// <param name="oldID"></param>
	/// <param name="newID"></param>
	public void HandleSingleGeoUpdate(string oldGeoID, string newGeoID){

		 m_IGeographyDAO.SingleGeoUpdate(oldGeoID, newGeoID);
	}



	public bool HandleDeleteSingleGeoByGID(string targetGID)
    {
		return m_IGeographyDAO.DeleteById(targetGID);
    }


	public void HandleDeleteMultipleGeoContent(List<string> targs)
	{
		m_IGeographyDAO.DeleteById(targs);
	}

	public void HandleDeleteAll()
    {
		m_IGeographyDAO.DeleteAll();
    }

	public int HandleCount()
    {
		return m_IGeographyDAO.Count();
    }

}//end GeographyService
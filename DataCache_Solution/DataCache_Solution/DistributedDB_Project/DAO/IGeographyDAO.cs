///////////////////////////////////////////////////////////
//  IGeographyDAO.cs
//  Implementation of the Interface IGeographyDAO
//  Generated by Enterprise Architect
//  Created on:      07-May-2021 2:59:27 PM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;

public interface IGeographyDAO: ICRUDDao<GeoRecord, string>
{
    void DeleteById(List<string> targs);
    void SingleGeoUpdate(string oldGID, string newGID);
}//end IGeographyDAO
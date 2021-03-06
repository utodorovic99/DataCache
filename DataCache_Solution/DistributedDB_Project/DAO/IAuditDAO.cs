///////////////////////////////////////////////////////////
//  IAuditDAO.cs
//  Implementation of the Interface IAuditDAO
//  Generated by Enterprise Architect
//  Created on:      07-May-2021 2:59:26 PM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;

public interface IAuditDAO: ICRUDDao<AuditRecord, string>
{
    IEnumerable<AuditRecord> DuplicatesAll();
    IEnumerable<AuditRecord> DuplicatesAllByGeo(string gID);

    IEnumerable<AuditRecord> MissesAll();

    IEnumerable<AuditRecord> MissesAllByGeo(string gID);

    IEnumerable<AuditRecord> DupsAndMissesByGeo(string gID);

}//end IAuditDAO
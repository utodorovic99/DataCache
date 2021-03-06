///////////////////////////////////////////////////////////
//  AuditDAOImpl.cs
//  Implementation of the Class AuditDAOImpl
//  Generated by Enterprise Architect
//  Created on:      07-May-2021 2:59:23 PM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;
using System.Data;
using DistributedDB_Project.Connection;
using DistributedDB_Project.DAO.Impl;
using DistributedDB_Project.Exceptions.ExceptionAbstraction;

public class AuditDAOImpl : IAuditDAO {

	public AuditDAOImpl(){

	}

	~AuditDAOImpl(){

	}

    private AuditRecord LoadAuditRecordSingleByQuery(string query)
    {
        AuditRecord retVal;
        using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Prepare();
                using (IDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        retVal = new AuditRecord(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                    }
                    catch (ArgumentNullException)
                    {
                        return new AuditRecord();
                    }
                }
            }
        }
        return retVal;
    }

    private IEnumerable<AuditRecord> LoadAuditRecordsMultipleByQuery(string query)
    {
        List<AuditRecord> retVal = new List<AuditRecord>();
        using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Prepare();

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) retVal.Add(new AuditRecord(reader.GetString(0), reader.GetString(1), reader.GetInt32(2)));
                }

            }
        }
        return retVal;
    }

    public int Count()
    {
        string query = "SELECT COUNT(*) FROM CONSUMPTION_AUDIT ";                         

        using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Prepare();

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
    }

    public bool Delete(AuditRecord entity)
    {
        throw new ForbiddenOrderException("Each audit record is conneted to its EES record (geo-time context).\n" +
            "Are deleted automatically trough consumption delete");
    }

    public void DeleteAll()
    {
        throw new ForbiddenOrderException("Each audit record is conneted to its EES record (geo-time context).\n+" +
            "Are deleted automatically trough consumption delete");
    }

    public bool DeleteById(string id)
    {
        throw new ForbiddenOrderException("Each audit record is conneted to its EES record (geo-time context).\n+" +
            "Are deleted automatically trough consumption delete");
    }

    public bool ExistsById(string id)
    {
        using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
        {
            connection.Open();
            using (IDbCommand command = connection.CreateCommand())
            {

                string query = "SELECT ca.AID " +
                "FROM consumption_audited cad, consumption_audit ca " +
                "WHERE ca.AID = cad.AID " +
                "AND ca.AID = " + id;

                command.CommandText = query;
                command.Prepare();
                return command.ExecuteScalar() != null;
            }
        }
    }

    public IEnumerable<AuditRecord> FindAll()
    {
        string query=   "SELECT ee.GID, ee.time_stamp, ca.dupval "+
                        "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                        "WHERE ee.RECID = cad.RECID " +
                        "AND cad.AID = ca.AID ";

        return LoadAuditRecordsMultipleByQuery(query);
    }

    public IEnumerable<AuditRecord> FindAllById(IEnumerable<string> ids)
    {

        string query = String.Format
                       ("SELECT ee.GID, ee.time_stamp, ca.dupval " +
                        "FROM consumption_audited cad LEFT OUTER JOIN ees ee ON cad.recid = ee.recid "+
                            " LEFT OUTER JOIN consumption_audit ca ON cad.aid = ca.aid " +
                        "WHERE cad.AID IN {0} ", CommonImpl.FormatComplexArgument(ids));

        return LoadAuditRecordsMultipleByQuery(query);
    }

    public AuditRecord FindById(string id)
    {
        string query = "SELECT ee.GID, ee.time_stamp, ca.dupval " +
                       "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                       "WHERE ee.RECID = cad.RECID " +
                       "AND cad.AID = ca.AID "+
                       "AND ca.AID = "+id;

        return LoadAuditRecordSingleByQuery(query);
    }

    public void Save(AuditRecord entity)
    {
        throw new ForbiddenOrderException("Each audit record is conneted to its EES record (geo-time context).\nTry adding new record via invalid ConsumptionRecord");
    }

    public void SaveAll(IEnumerable<AuditRecord> entities)
    {
        throw new ForbiddenOrderException("Each audit record is conneted to its EES record (geo-time context).\nTry adding new record via invalid ConsumptionRecord");
    }

    public IEnumerable<AuditRecord> DuplicatesAll()
    {
        string query = "SELECT ee.GID, ee.time_stamp, ca.dupval " +
                       "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                       "WHERE ee.RECID = cad.RECID " +
                       "AND cad.AID = ca.AID "+
                       "AND ca.DUPVAL <> -1 ";

        return LoadAuditRecordsMultipleByQuery(query);
    }
    public IEnumerable<AuditRecord> DuplicatesAllByGeo(string gID)
    {
        string query = String.Format(   "SELECT ee.GID, ee.time_stamp, ca.dupval " +
                                        "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                                        "WHERE ee.RECID = cad.RECID " +
                                        "AND cad.AID = ca.AID " +
                                        "AND ca.DUPVAL <> -1 "+
                                        "AND ee.GID = '{0}' ", gID);

        return LoadAuditRecordsMultipleByQuery(query);
    }
    public IEnumerable<AuditRecord> MissesAll()
    {
        string query = "SELECT ee.GID, ee.time_stamp, ca.dupval " +
                       "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                       "WHERE ee.RECID = cad.RECID " +
                       "AND cad.AID = ca.AID " +
                       "AND ca.DUPVAL = -1 ";

        return LoadAuditRecordsMultipleByQuery(query);
    }
    public IEnumerable<AuditRecord> MissesAllByGeo(string gID)
    {
        string query = String.Format
                              ("SELECT ee.GID, ee.time_stamp, ca.dupval " +
                               "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                               "WHERE ee.RECID = cad.RECID " +
                               "AND cad.AID = ca.AID " +
                               "AND ca.DUPVAL = -1 " +
                               "AND ee.GID = '{0}' ", gID);

        return LoadAuditRecordsMultipleByQuery(query);
    }

    public IEnumerable<AuditRecord> DupsAndMissesByGeo(string gID)
    {
        string query = String.Format
                              ("SELECT ee.GID, ee.time_stamp, ca.dupval " +
                               "FROM ees ee, consumption_audited cad, consumption_audit ca " +
                               "WHERE ee.RECID = cad.RECID " +
                               "AND cad.AID = ca.AID " +
                               "AND ee.GID = '{0}' ", gID);

        return LoadAuditRecordsMultipleByQuery(query);
    }

}//end AuditDAOImpl
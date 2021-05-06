﻿///////////////////////////////////////////////////////////
//  XMLHandler.cs
//  Implementation of the Class XMLHandler
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:25 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;
using Common_Project.Classes;
using FileControler_Project.Enums;

namespace FileControler_Project.Handlers.XMLHandler.Classes
{
    public class XMLHandler
{

	public XMLHandler()
	{

	}

	~XMLHandler()
	{

	}

	private EXMLElementStatus IsOstvConsumptionXMLEmentValid(XmlNode element)   // For Stavka checking child elements 
	{
		int satFound = 0, loadFound = 0, oblastFound = 0, unknownFound = 0;
		foreach (XmlNode childNode in element.ChildNodes)
		{
			if (childNode.NodeType == XmlNodeType.Element)
			{
				switch ((childNode as XmlNode).Name)
				{
					//Place to add new elements in single STAVKA
					case "SAT": { ++satFound; break; }
					case "LOAD": { ++loadFound; break; }
					case "OBLAST": { ++oblastFound; break; }
					default: { ++unknownFound; break; }
				}
			}
		}

		//Look EXMLElementStatus for status description
		if (satFound == 0 && loadFound == 0 && oblastFound == 0 && unknownFound == 0) return EXMLElementStatus.Fail;
		if (satFound == 1 && loadFound == 0 && oblastFound == 1 && unknownFound == 0) return EXMLElementStatus.PartialValid;
		if (satFound == 1 && loadFound == 1 && oblastFound == 1 && unknownFound == 0)
		{
			if (unknownFound == 0) return EXMLElementStatus.Valid;
			else return EXMLElementStatus.Overflow;
		}
		return EXMLElementStatus.PartialDump;
	}

	private ConsumptionRecord ParseXMLConsumptionRecord(XmlNode xmlNode, string dateTimeBase)
	{
		ConsumptionRecord retCRecord = new ConsumptionRecord();
		string elemContent = "";
		int tmpParse;
		foreach (XmlNode childNode in xmlNode.ChildNodes)
		{
			if (xmlNode.NodeType == XmlNodeType.Element)
			{
				elemContent = childNode.InnerText;
				switch ((childNode as XmlNode).Name)
				{
					//Place to add new elements to handle in single STAVKA
					case "SAT":
						{
							if (Int32.TryParse(elemContent, out tmpParse) && tmpParse > 0 && tmpParse <= 24)
							{
								retCRecord.TimeStamp = dateTimeBase + elemContent;
							}
							break;
						}
					case "LOAD":
						{
							if (Int32.TryParse(elemContent, out tmpParse) && tmpParse > 0)
							{
								retCRecord.MWh = Int32.Parse(elemContent);
							}
							break;
						}
					case "OBLAST": { retCRecord.GID = elemContent; break; }
					default: {/*Place to handle not supported node types*/ break; }
				}
			}

		}

		return retCRecord;
	}

	/// 
	/// <param name="path"></param>
	public Tuple<EFileLoadStatus, List<ConsumptionRecord>> XMLOstvConsumptionRead(FileInfo fileInfo)
	{

		string dateTimeBase = "";
		{
			char[] splitWordsBy = "_.".ToArray();
			string[] splitParts = fileInfo.Name.Split(splitWordsBy, StringSplitOptions.RemoveEmptyEntries);
			dateTimeBase = splitParts[1] + "-" + splitParts[2] + "-" + splitParts[3] + "-";
		}

		List<ConsumptionRecord> readedElems = new List<ConsumptionRecord>();

		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(fileInfo.FullName);
		if (xmlDocument == null) return new Tuple<EFileLoadStatus, List<ConsumptionRecord>>(EFileLoadStatus.OpeningFailed, readedElems);

		int elemsAcceptable = 0;
		bool corruptedElems = false;
		foreach (XmlNode xmlNode in xmlDocument.DocumentElement)
		{
			if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.Name == "STAVKA")
			{
				switch (IsOstvConsumptionXMLEmentValid(xmlNode))
				{
					case EXMLElementStatus.Fail:
						{
							corruptedElems = true;
							break;
						}
					case EXMLElementStatus.Overflow:    // Consired as acceptable
						{
							++elemsAcceptable;
							readedElems.Add(ParseXMLConsumptionRecord(xmlNode, dateTimeBase));
							break;
						}
					case EXMLElementStatus.PartialValid:
						{
							++elemsAcceptable;
							readedElems.Add(ParseXMLConsumptionRecord(xmlNode, dateTimeBase));
							break;
						}
					case EXMLElementStatus.PartialDump:
						{
							corruptedElems = true;
							break;
						}
					case EXMLElementStatus.Valid:
						{
							++elemsAcceptable;
							readedElems.Add(ParseXMLConsumptionRecord(xmlNode, dateTimeBase));
							break;
						}
					default:
						{
							break;
						}
				}
			}

		}

		if (corruptedElems && elemsAcceptable > 0) return new Tuple<EFileLoadStatus, List<ConsumptionRecord>>
																		(EFileLoadStatus.PartialReadSuccess, readedElems);

		if (corruptedElems && elemsAcceptable == 0) return new Tuple<EFileLoadStatus, List<ConsumptionRecord>>
																(EFileLoadStatus.InvalidFileStructure, readedElems);


		return new Tuple<EFileLoadStatus, List<ConsumptionRecord>>                              // Risky
												(EFileLoadStatus.Success, readedElems);
	}

}//end XMLHandler

}
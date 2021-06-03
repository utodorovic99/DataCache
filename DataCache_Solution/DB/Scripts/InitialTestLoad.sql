DELETE consumption_audited;
DELETE consumption_recorded;
DELETE consumption_audit;
DELETE consumption;
DELETE ees;
DELETE geography_subsystem;
commit;

--GEOGRAPHY
INSERT INTO GEOGRAPHY_SUBSYSTEM (GID, GNAME)
VALUES ('SRB','SRB');

INSERT INTO GEOGRAPHY_SUBSYSTEM (GID, GNAME)
VALUES ('2','MNE');

INSERT INTO GEOGRAPHY_SUBSYSTEM (GID, GNAME)
VALUES ('BIH','BIH');

INSERT INTO GEOGRAPHY_SUBSYSTEM (GID, GNAME)
VALUES ('3','USA');	

--EES 
    --SRB
        --'2021-01-01-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (1, '2021-01-01-01', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (2, '2021-01-01-02', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (3, '2021-01-01-03', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (4, '2021-01-01-04', 'SRB');

        --'2021-01-03-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (5, '2021-01-03-01', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (6, '2021-01-03-02', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (7, '2021-01-03-05', 'SRB');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (8, '2021-01-03-22', 'SRB');

    --MNE
            --'2021-01-01-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (9, '2021-01-01-02', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (10, '2021-01-01-04', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (11, '2021-01-01-06', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (12, '2021-01-01-08', '2');

        --'2021-01-03-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (13, '2021-01-03-11', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (14, '2021-01-03-12', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (15, '2021-01-03-15', '2');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (16, '2021-01-03-07', '2');
    
    --BIH
    
     --'2021-02-04-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (17, '2021-02-04-02', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (18, '2021-02-04-04', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (19, '2021-02-04-06', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (20, '2021-02-04-08', 'BIH');

        --'2021-01-08-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (21, '2021-01-08-11', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (22, '2021-01-08-12', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (23, '2021-01-08-15', 'BIH');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (24, '2021-01-08-07', 'BIH');

INSERT INTO consumption_audit(AID, DUPVAL)
VALUES(121, -1);
INSERT INTO consumption_audit(AID, DUPVAL)
VALUES(122, -1);
INSERT INTO consumption_audited (AID, RECID)
VALUES(121, 21);
INSERT INTO consumption_audited (AID, RECID)
VALUES(122, 22);
commit;
    
    --USA
    --'2021-03-04-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (25, '2021-03-04-21', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (26, '2021-03-04-14', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (27, '2021-03-04-16', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (28, '2021-03-04-18', '3');

        --'2021-05-08-HOUR'
INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (29, '2021-05-08-11', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (30, '2021-05-08-14', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (31, '2021-05-08-12', '3');

INSERT INTO EES (RECID, TIME_STAMP, GID)
VALUES (32, '2021-05-08-17', '3');	

--Consumption & Audit
INSERT INTO CONSUMPTION (CID, MWH)
VALUES(1,1);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(1,11);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(1,1);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(1,1);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(2,2);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(2,22);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(2,2);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(2,2);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(3,3);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(3,33);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(3,3);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(3,3);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(4,4);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(4,44);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(4,4);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(4,4);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(5,5);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(5,55);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(6,555);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(7,5559);

INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(5,5);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(5,5);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(6,5);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(7,5);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(6,6);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(8,66);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(9,669);
INSERT INTO CONSUMPTION_AUDIT (AID, DUPVAL)
VALUES(10,101011);

INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(6,6);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(8,6);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(9,6);
INSERT INTO CONSUMPTION_AUDITED(AID, RECID)
VALUES(10,6);

INSERT INTO CONSUMPTION (CID, MWH)
VALUES(7,88);
INSERT INTO CONSUMPTION (CID, MWH)
VALUES(8,88);
INSERT INTO CONSUMPTION (CID, MWH)
VALUES(9,999);
INSERT INTO CONSUMPTION (CID, MWH)
VALUES(10,1010);

INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(7,17);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(8,18);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(9,19);
INSERT INTO CONSUMPTION_RECORDED(CID, RECID)
VALUES(10,20);

UPDATE CONSUMPTION
SET MWH=MwH*2+3;
commit;
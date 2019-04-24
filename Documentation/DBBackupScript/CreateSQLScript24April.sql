CREATE DATABASE  IF NOT EXISTS `crowsoftdb` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `crowsoftdb`;
-- MySQL dump 10.13  Distrib 5.7.25, for Win64 (x86_64)
--
-- Host: localhost    Database: crowsoftdb
-- ------------------------------------------------------
-- Server version	5.7.25-0ubuntu0.16.04.2

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `RoleId` varchar(127) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  `ConcurrencyStamp` longtext,
  `NormalizedName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
INSERT INTO `AspNetRoles` VALUES ('1','Admin',NULL,'ADMIN'),('2','Client',NULL,'CLIENT'),('3','User',NULL,'USER');
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
INSERT INTO `AspNetUserRoles` VALUES ('06bf2dc1-6d00-446e-9f39-4402ed9a6b3c','1'),('142331f4-23d2-4835-9b77-b6a3d38223a2','1'),('89d43afb-2937-487f-9802-e9002150a209','1'),('dbb1c75f-5d73-4363-8748-1e86ed524902','1');
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES ('06bf2dc1-6d00-446e-9f39-4402ed9a6b3c','L00131535@student.lyit.ie',0,'AQAAAAEAACcQAAAAEP/0KTgTIE2NSsoJyAZN84cW1veYnrWY0gGHJ2AxGrAvMOCveaOLzseEbqIByDEODA==','Q74QDTLVKXNCJNHBUXSSZHKM6AEM32TP','080000000',0,0,NULL,1,0,'L00131535@student.lyit.ie','L00131535@STUDENT.LYIT.IE','L00131535@STUDENT.LYIT.IE','66bb1e88-ada5-4fe3-9192-456235a6fcba'),('142331f4-23d2-4835-9b77-b6a3d38223a2','done@email.com',0,'AQAAAAEAACcQAAAAEN36h0fH5/YMaL0ws5PcLzzTr2FN5hxbbba+Sq+CLKm82E+cNoVwVpcfr2XwRgp3hA==','GLS3S7QNDR3B7L6DA3WUWCUDBQGMZ2XE','12345678',0,0,NULL,1,0,'done@email.com','DONE@EMAIL.COM','DONE@EMAIL.COM',NULL),('37054b28-8530-4ed9-83ac-6dbe311ee041','joe@blog.com',0,'AQAAAAEAACcQAAAAEKpWqQ8cKjX909L0Z+cwvXiSDiZcagp+Lxb5RXgCILaee6EXIgz7gxeP/z726xZICg==','PSPHWBJ4ARLEREUFDKG2Y3V5AN2O63MS',NULL,0,0,NULL,1,0,'joe@blog.com','JOE@BLOG.COM','JOE@BLOG.COM',NULL),('3f33fc19-b57c-440b-a8fd-1b6a5bfc67a2','l00000@lyit.ie',0,'AQAAAAEAACcQAAAAEMJXe1yqRinKxtCrJf2Kj7LO317pXsb2Br3hADjzREs2yJWumYprNArMc2YT+lnBiA==','44SK4JCEIBL3APQGSD25TPHXUM2NBSDL','123456789',0,0,NULL,1,0,'l00000@lyit.ie','L00000@LYIT.IE','L00000@LYIT.IE',NULL),('4343777b-39a4-4d7e-bc41-82e4bf237031','logan@email.com',0,'AQAAAAEAACcQAAAAEBvAbsNYrRc0UrPubaDH48cx4jUjt8ugciHFFF9a1D3MIdz6PbExfUab26y/rEytVQ==','T56N4CALQK7TTSO2JASOIF2Z2O3JPK25',NULL,0,0,NULL,1,0,'logan@email.com','LOGAN@EMAIL.COM','LOGAN@EMAIL.COM',NULL),('572a92f5-1cfc-4b35-b134-8775fc71160b','paulsecond@blog.com',0,'AQAAAAEAACcQAAAAEBqxl1Kr3jzgLC9q5w+7elLScTOjapqbD9ByiZf/YLus/8XAFe5Eolw12QQN6BFOIg==','MIXXJY5JLFQPZ3JCI5MHVZGVTHQQCQSR',NULL,0,0,NULL,1,0,'paulsecond@blog.com','PAULSECOND@BLOG.COM','PAULSECOND@BLOG.COM',NULL),('695caec6-c8a6-4731-8375-9427c04bf82d','suceeded@hotmail.com',0,'AQAAAAEAACcQAAAAED41FIphE4akfEp44sIvS70McRZEUESmpYDs2iF/EkEahJMDrv7KGY3hKgH0gAMPYA==','KITKYN276XWOZ3GAGJAXOTTOK7MXLGM3',NULL,0,0,NULL,1,0,'suceeded@hotmail.com','SUCEEDED@HOTMAIL.COM','SUCEEDED@HOTMAIL.COM',NULL),('89d43afb-2937-487f-9802-e9002150a209','L00113360@student.lyit.ie',0,'AQAAAAEAACcQAAAAEHD7PzzMgCSQ8cl2zklH06aaIrnWmbU4msAnKdr2rB6QGdxEL7BMiZakIQwLukKpcg==','4B3VOFMIJDKWYVCIL33LFQNC7RIH7E4F','0123456',0,0,NULL,1,0,'L00113360@student.lyit.ie','L00113360@STUDENT.LYIT.IE','L00113360@STUDENT.LYIT.IE','b7843dfd-ea21-441c-977d-18bc86a57528'),('8b49533a-ec9b-4ea2-9757-c99e8ceaada1','father@lamb.com',0,'AQAAAAEAACcQAAAAEGn8aIX6dCNT6zC89L1oKKQexaRfnTvikOL/uJt0PRZFHYcbgfWD/zbPt4P7knTvZA==','GBPMCA2QJQXPHWL2NB7VSJJVZZ55UNLD',NULL,0,0,NULL,1,0,'father@lamb.com','FATHER@LAMB.COM','FATHER@LAMB.COM',NULL),('97267b0a-4d86-49c7-a1a8-f1a7fe04a215','paul@blog.com',0,'AQAAAAEAACcQAAAAEBwdzwCp2PdJvwi/xx7+2t3x+fCcZjqhz5OnzXUvrkOpnJ1hfr31OZGZC+1mNHVWEg==','S6BZHIFUG3JBDUU3IUTUCK5RTCR4X3ZM',NULL,0,0,NULL,1,0,'paul@blog.com','PAUL@BLOG.COM','PAUL@BLOG.COM',NULL),('a04a7e3e-99f0-45b3-99b0-3f8203618ef9','test@hotmail.com',0,'AQAAAAEAACcQAAAAEJe8nOHz+K9mG0KvFw9+SM8YLRyGs3r/f4AscEYrMzsqQBb3oinupI79DjW+oWn5+A==','VZW3QOJURD2I7FCM6UWBTVSCADTPSNU4',NULL,0,0,NULL,1,0,'test@hotmail.com','TEST@HOTMAIL.COM','TEST@HOTMAIL.COM',NULL),('bd50f2f4-0fa2-48a4-b7c2-b33fe081e5bd','john@doe.com',0,'AQAAAAEAACcQAAAAEJ7GX9q1YosRz4jBvEqzld/QIp82lrZNPvP3XzK4MLDDfEVvbucxTNqO4RCERjHr4g==','6V5S6ABQ3TUEZ6S35P5S5X2LRV36CJQC',NULL,0,0,NULL,1,0,'john@doe.com','JOHN@DOE.COM','JOHN@DOE.COM',NULL),('ce685af7-4050-4238-b01a-695b720c7725','dwanyeTheBlockjohnson@hotmale.com',0,'AQAAAAEAACcQAAAAEFgI1W14YvfuzJOE0+zX5V6a0oDYT7symUX03Xpu9rhn2IBVcQgniP8ULi3pE82sMA==','LHANPCRAXPERLNJO67TU2Z4OHR2IDIZF',NULL,0,0,NULL,1,0,'dwanyeTheBlockjohnson@hotmale.com','DWANYETHEBLOCKJOHNSON@HOTMALE.COM','DWANYETHEBLOCKJOHNSON@HOTMALE.COM',NULL),('dbb1c75f-5d73-4363-8748-1e86ed524902','admin@crowsoft.com',0,'AQAAAAEAACcQAAAAELZ+2tWu9EslsOCTgOfkBWufwR6g71shxlicx/ohxUWlHS2LaBjYvI8L2E68q8EzyQ==','Q4FRS2PRFORSCWRVJW4B6STEN5FLARTR','09000000',0,0,NULL,1,0,'admin@crowsoft.com','ADMIN@CROWSOFT.COM','ADMIN@CROWSOFT.COM','8b6480c7-53d4-420c-a5fd-a6883a81ef1a'),('e4fae73c-7185-451f-9089-e79aa63da6a2','L0000000@email.com',0,'AQAAAAEAACcQAAAAEBpIVg742oYvfMgzMaLdzLSNdoPHg5jZAtyB8+G0TL8p+8sptdDyfTJqEyPtRzY4vw==','T4VR3LAISA76IRIC6LJZEQFS7YEIEWTU','123465489',0,0,NULL,1,0,'L0000000@email.com','L0000000@EMAIL.COM','L0000000@EMAIL.COM',NULL);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BuildingFeatures`
--

DROP TABLE IF EXISTS `BuildingFeatures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BuildingFeatures` (
  `idBuildingFeatures` int(11) NOT NULL AUTO_INCREMENT,
  `FeatureDescription` varchar(100) NOT NULL,
  `Comments` varchar(150) DEFAULT NULL,
  `Quantity` decimal(10,4) NOT NULL,
  `UnitPrice` decimal(10,2) NOT NULL,
  `TotalCost` decimal(12,2) DEFAULT NULL,
  `BuildingQuote_idBuildingQuote` int(11) NOT NULL,
  `DefaultFeature_idDefaultFeature` int(11) NOT NULL,
  PRIMARY KEY (`idBuildingFeatures`,`BuildingQuote_idBuildingQuote`,`DefaultFeature_idDefaultFeature`),
  UNIQUE KEY `idBuildingFeatures_UNIQUE` (`idBuildingFeatures`),
  KEY `fk_BuildingFeatures_BuildingQuote1_idx` (`BuildingQuote_idBuildingQuote`),
  KEY `fk_BuildingFeatures_DefaultFeature1_idx` (`DefaultFeature_idDefaultFeature`),
  CONSTRAINT `fk_BuildingFeatures_BuildingQuote1` FOREIGN KEY (`BuildingQuote_idBuildingQuote`) REFERENCES `BuildingQuote` (`idBuildingQuote`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_BuildingFeatures_DefaultFeature1` FOREIGN KEY (`DefaultFeature_idDefaultFeature`) REFERENCES `DefaultFeature` (`idDefaultFeature`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BuildingFeatures`
--

LOCK TABLES `BuildingFeatures` WRITE;
/*!40000 ALTER TABLE `BuildingFeatures` DISABLE KEYS */;
/*!40000 ALTER TABLE `BuildingFeatures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BuildingImage`
--

DROP TABLE IF EXISTS `BuildingImage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BuildingImage` (
  `idBuildingImage` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(150) DEFAULT NULL,
  `ImagePath` varchar(200) DEFAULT NULL,
  `BuildingQuote_idBuildingQuote` int(11) NOT NULL,
  PRIMARY KEY (`idBuildingImage`,`BuildingQuote_idBuildingQuote`),
  UNIQUE KEY `idBuildingImage_UNIQUE` (`idBuildingImage`),
  KEY `fk_BuildingImage_BuildingQuote1_idx` (`BuildingQuote_idBuildingQuote`),
  CONSTRAINT `fk_BuildingImage_BuildingQuote1` FOREIGN KEY (`BuildingQuote_idBuildingQuote`) REFERENCES `BuildingQuote` (`idBuildingQuote`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BuildingImage`
--

LOCK TABLES `BuildingImage` WRITE;
/*!40000 ALTER TABLE `BuildingImage` DISABLE KEYS */;
/*!40000 ALTER TABLE `BuildingImage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `BuildingQuote`
--

DROP TABLE IF EXISTS `BuildingQuote`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `BuildingQuote` (
  `idBuildingQuote` int(11) NOT NULL AUTO_INCREMENT,
  `UserAccount_idUserAccount` int(11) NOT NULL,
  `Description` varchar(150) NOT NULL,
  `MeasurementType` varchar(25) NOT NULL,
  `Height` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Depth` int(11) NOT NULL,
  `PurposeOfBuilding` varchar(150) DEFAULT NULL,
  `BuildingSize` int(11) DEFAULT NULL,
  `TotalCost` decimal(12,2) DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `Status` varchar(10) DEFAULT NULL,
  `TimeFrame` int(11) DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL,
  `UpdatedById` int(11) DEFAULT NULL,
  PRIMARY KEY (`idBuildingQuote`),
  UNIQUE KEY `idBuildingQuote_UNIQUE` (`idBuildingQuote`),
  KEY `fk_BuildingQuote_UserAccount_idx` (`UserAccount_idUserAccount`),
  CONSTRAINT `fk_BuildingQuote_UserAccount` FOREIGN KEY (`UserAccount_idUserAccount`) REFERENCES `UserAccount` (`idUserAccount`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `BuildingQuote`
--

LOCK TABLES `BuildingQuote` WRITE;
/*!40000 ALTER TABLE `BuildingQuote` DISABLE KEYS */;
/*!40000 ALTER TABLE `BuildingQuote` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `DefaultFeature`
--

DROP TABLE IF EXISTS `DefaultFeature`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `DefaultFeature` (
  `idDefaultFeature` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(100) NOT NULL,
  `Measurement` varchar(20) NOT NULL,
  `UnitPrice` decimal(8,2) NOT NULL,
  `IsDefaultFeature` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`idDefaultFeature`),
  UNIQUE KEY `idBuildingCost_UNIQUE` (`idDefaultFeature`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `DefaultFeature`
--

LOCK TABLES `DefaultFeature` WRITE;
/*!40000 ALTER TABLE `DefaultFeature` DISABLE KEYS */;
INSERT INTO `DefaultFeature` VALUES (2,'Building Size','SqrMeters',50.00,1),(3,'Labour Cost','Hour',60.00,0);
/*!40000 ALTER TABLE `DefaultFeature` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Dummy`
--

DROP TABLE IF EXISTS `Dummy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Dummy` (
  `PersonID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `LastName` varchar(255) DEFAULT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `City` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`PersonID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Dummy`
--

LOCK TABLES `Dummy` WRITE;
/*!40000 ALTER TABLE `Dummy` DISABLE KEYS */;
INSERT INTO `Dummy` VALUES (1,'John ','Doe','Somewhere','Donegal');
/*!40000 ALTER TABLE `Dummy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserAccount`
--

DROP TABLE IF EXISTS `UserAccount`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserAccount` (
  `idUserAccount` int(11) NOT NULL AUTO_INCREMENT,
  `EmailAddress` varchar(150) NOT NULL COMMENT 'User email address used as username as well. ',
  `FirstName` varchar(60) NOT NULL,
  `LastName` varchar(60) NOT NULL,
  `TelephoneNo` varchar(45) NOT NULL,
  `AddressLine` varchar(150) DEFAULT NULL,
  `County` varchar(45) DEFAULT NULL,
  `Country` varchar(60) DEFAULT NULL,
  `EirCode` varchar(20) DEFAULT NULL,
  `CompanyName` varchar(45) DEFAULT NULL,
  `TypeUser` varchar(15) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `AspNetUserID` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`idUserAccount`),
  UNIQUE KEY `idUserAccount_UNIQUE` (`idUserAccount`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserAccount`
--

LOCK TABLES `UserAccount` WRITE;
/*!40000 ALTER TABLE `UserAccount` DISABLE KEYS */;
INSERT INTO `UserAccount` VALUES (8,'test@email.com','Joe ','Blog','9998887777','10 address','Donegal','Ireland',NULL,NULL,'Client','2019-04-09 11:16:11',NULL),(9,'father@lamb.com','Father','Had a lamb','12345678','222 street','Antrim','Ireland',NULL,NULL,'User','2019-04-12 15:59:16',NULL),(10,'logan@email.com','Logan','Blog','12345678','222 street','Dublin','Ireland',NULL,'My Company','Client','2019-04-12 16:15:10',NULL),(24,'done@email.com','Done','Deal','12345678','222 street','Donegal','Ireland',NULL,'Private','Client','2019-04-13 14:01:13','142331f4-23d2-4835-9b77-b6a3d38223a2'),(26,'l00000@lyit.ie','Lllllllll','sssssss','123456789','123 stress','Antrim','Ireland',NULL,'Private','Client','2019-04-17 16:10:15','3f33fc19-b57c-440b-a8fd-1b6a5bfc67a2'),(27,'L0000000@email.com','LP00000000','Noe one','123465489','222 street','Antrim','Ireland',NULL,'Private','Client','2019-04-17 16:13:41','e4fae73c-7185-451f-9089-e79aa63da6a2'),(29,'L00131535@student.lyit.ie','Charles','Aylward','080000000','222 street','Donegal','Ireland',NULL,'LYIT','Client','2019-04-18 08:52:13','06bf2dc1-6d00-446e-9f39-4402ed9a6b3c'),(30,'admin@crowsoft.com','Admin User','Admin','09000000','No address','Donegal','Ireland',NULL,'CrowSoft','Client','2019-04-18 10:11:04','dbb1c75f-5d73-4363-8748-1e86ed524902'),(37,'L00113360@student.lyit.ie','Liam','Whorriskey','0123456','Letterkenny','Donegal','Ireland',NULL,'LYIT','Client','2019-04-20 19:03:47','89d43afb-2937-487f-9802-e9002150a209');
/*!40000 ALTER TABLE `UserAccount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8 NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-24 13:21:39

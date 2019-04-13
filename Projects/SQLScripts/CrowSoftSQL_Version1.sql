CREATE DATABASE `crowsoftdb` /*!40100 DEFAULT CHARACTER SET latin1 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8 NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `AspNetRoles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `ConcurrencyStamp` longtext,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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

CREATE TABLE `DefaultFeature` (
  `idDefaultFeature` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(100) NOT NULL,
  `Measurement` varchar(20) NOT NULL,
  `UnitPrice` decimal(8,2) NOT NULL,
  `DefaultFeature` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`idDefaultFeature`),
  UNIQUE KEY `idBuildingCost_UNIQUE` (`idDefaultFeature`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Dummy` (
  `PersonID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `LastName` varchar(255) DEFAULT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `City` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`PersonID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

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
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

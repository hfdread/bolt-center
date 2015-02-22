-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.28 - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2014-04-05 08:47:23
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for jim_boltcenter
CREATE DATABASE IF NOT EXISTS `jim_boltcenter` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `jim_boltcenter`;


-- Dumping structure for procedure jim_boltcenter.Clean Transaction Tables
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `Clean Transaction Tables`()
BEGIN
SET foreign_key_checks=0;

TRUNCATE TABLE salesinvoice;
TRUNCATE TABLE salesinvoicedetails;

TRUNCATE TABLE receipt;
TRUNCATE TABLE receiptdetails;
END//
DELIMITER ;


-- Dumping structure for procedure jim_boltcenter.CLEANTABLES_$
DELIMITER //
CREATE DEFINER=`root`@`localhost` PROCEDURE `CLEANTABLES_$`()
BEGIN
SET foreign_key_checks=0;

TRUNCATE TABLE contacts;
TRUNCATE TABLE forwarders;
TRUNCATE TABLE invoicetype;
TRUNCATE TABLE item;
TRUNCATE TABLE itemsizes;

TRUNCATE TABLE salesinvoice;
TRUNCATE TABLE salesinvoicedetails;

TRUNCATE TABLE receipt;
TRUNCATE TABLE receiptdetails;

TRUNCATE TABLE weight;

END//
DELIMITER ;


-- Dumping structure for table jim_boltcenter.contacts
CREATE TABLE IF NOT EXISTS `contacts` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(50) DEFAULT NULL,
  `CompanyContact` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `CompanyAddress` varchar(100) DEFAULT NULL,
  `Agent` varchar(50) DEFAULT NULL,
  `MobileNo` varchar(50) DEFAULT NULL,
  `PhoneNo` varchar(50) DEFAULT NULL,
  `FaxNo` varchar(50) DEFAULT NULL,
  `tin_number` varchar(50) DEFAULT NULL,
  `bir_acct` varchar(50) DEFAULT NULL,
  `sec_acct` varchar(50) DEFAULT NULL,
  `nonvat_reg` varchar(50) DEFAULT NULL,
  `vat_reg` varchar(50) DEFAULT NULL,
  `Type` int(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='1 - supplier\r\n2 - customer\r\n3 - agent';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.forwarders
CREATE TABLE IF NOT EXISTS `forwarders` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(50) NOT NULL,
  `Details` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.invoicetype
CREATE TABLE IF NOT EXISTS `invoicetype` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) NOT NULL,
  `Code` varchar(5) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='types of invoices';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.item
CREATE TABLE IF NOT EXISTS `item` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `Code` varchar(50) DEFAULT NULL,
  `Unit` varchar(20) NOT NULL,
  `Unit2` varchar(10) NOT NULL,
  `SizeID` int(10) unsigned DEFAULT NULL,
  `UnitPrice` double NOT NULL,
  `UnitPrice_2` double DEFAULT NULL,
  `LastPrice` double DEFAULT NULL,
  `RetailPrice` double DEFAULT NULL,
  `LowThreshold` int(10) DEFAULT NULL,
  `OnHand` int(10) DEFAULT NULL,
  `OnHandWeight` int(10) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_item_itemsizes` (`SizeID`),
  CONSTRAINT `FK_item_itemsizes` FOREIGN KEY (`SizeID`) REFERENCES `itemsizes` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='unitprice_2 - price/weight\r\nunitprice - price/piece\r\nunit2 - weight data';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.itemprofile
CREATE TABLE IF NOT EXISTS `itemprofile` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `sourceItem` int(10) unsigned NOT NULL,
  `targetItem` int(10) unsigned NOT NULL,
  `conversion` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_itemprofile_item` (`sourceItem`),
  KEY `FK2_itemprofile_item` (`targetItem`),
  CONSTRAINT `FK2_itemprofile_item` FOREIGN KEY (`targetItem`) REFERENCES `item` (`ID`),
  CONSTRAINT `FK_itemprofile_item` FOREIGN KEY (`sourceItem`) REFERENCES `item` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.itemsizes
CREATE TABLE IF NOT EXISTS `itemsizes` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.receipt
CREATE TABLE IF NOT EXISTS `receipt` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Customer` int(10) unsigned NOT NULL,
  `Agent` int(10) unsigned DEFAULT NULL,
  `Receipt_Date` datetime DEFAULT NULL,
  `isDeleted` tinyint(1) unsigned NOT NULL,
  `ReceiptAmount` double NOT NULL,
  `PaidAmount` double NOT NULL,
  `PO` varchar(15) DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_receipt_contacts` (`Customer`),
  KEY `FK2_receipt_contacts` (`Agent`),
  CONSTRAINT `FK2_receipt_contacts` FOREIGN KEY (`Agent`) REFERENCES `contacts` (`ID`),
  CONSTRAINT `FK_receipt_contacts` FOREIGN KEY (`Customer`) REFERENCES `contacts` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.receiptdetails
CREATE TABLE IF NOT EXISTS `receiptdetails` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ItemIndex` int(10) unsigned NOT NULL DEFAULT '0',
  `OR_Number` int(10) unsigned NOT NULL,
  `QTY` int(10) NOT NULL,
  `ItemID` int(10) unsigned NOT NULL,
  `UnitPrice` double NOT NULL,
  `Discount` varchar(100) DEFAULT NULL,
  `SubTotal` double NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_receiptdetails_receipt` (`OR_Number`),
  KEY `FK_receiptdetails_item` (`ItemID`),
  CONSTRAINT `FK_receiptdetails_item` FOREIGN KEY (`ItemID`) REFERENCES `item` (`ID`),
  CONSTRAINT `FK_receiptdetails_receipt` FOREIGN KEY (`OR_Number`) REFERENCES `receipt` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.retailinvoice
CREATE TABLE IF NOT EXISTS `retailinvoice` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ORNumber` int(10) NOT NULL,
  `Date` datetime NOT NULL,
  `Customer` varchar(30) DEFAULT NULL,
  `TIN` varchar(30) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `BusinessStyle` varchar(50) DEFAULT NULL,
  `Terms` varchar(50) DEFAULT NULL,
  `OSCA_PWD_ID` varchar(50) DEFAULT NULL,
  `VatAmt` double NOT NULL,
  `TotalAmt` double NOT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.retailinvoicedetails
CREATE TABLE IF NOT EXISTS `retailinvoicedetails` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `retailinvoice_ID` int(10) unsigned NOT NULL,
  `ItemID` int(10) unsigned NOT NULL,
  `QTY` int(10) DEFAULT NULL,
  `UnitPrice` int(10) DEFAULT NULL,
  `Amount` int(10) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_retailinvoicedetails_retailinvoice` (`retailinvoice_ID`),
  KEY `FK_retailinvoicedetails_item` (`ItemID`),
  CONSTRAINT `FK_retailinvoicedetails_item` FOREIGN KEY (`ItemID`) REFERENCES `item` (`ID`),
  CONSTRAINT `FK_retailinvoicedetails_retailinvoice` FOREIGN KEY (`retailinvoice_ID`) REFERENCES `retailinvoice` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.retailinvoice_series
CREATE TABLE IF NOT EXISTS `retailinvoice_series` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `start` int(10) NOT NULL,
  `end` int(10) NOT NULL,
  `current_in_stack` int(10) NOT NULL,
  `status` tinyint(1) NOT NULL DEFAULT '0',
  `invoicetypeID` int(10) unsigned NOT NULL,
  `Date` datetime NOT NULL,
  `DateUpdated` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_retailinvoice_series_invoicetype` (`invoicetypeID`),
  CONSTRAINT `FK_retailinvoice_series_invoicetype` FOREIGN KEY (`invoicetypeID`) REFERENCES `invoicetype` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.salesinvoice
CREATE TABLE IF NOT EXISTS `salesinvoice` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `invoice_type` int(10) NOT NULL,
  `InvoiceID` int(10) DEFAULT NULL,
  `SupplierID` int(10) unsigned NOT NULL,
  `ForwarderID` int(10) NOT NULL,
  `InvoiceDate` datetime NOT NULL,
  `CreateDate` datetime NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `ArrivalDate` datetime DEFAULT NULL,
  `FreightAmount` double DEFAULT NULL,
  `AR_Number` int(10) DEFAULT NULL,
  `QTY_Cart` int(10) DEFAULT NULL,
  `Invoice_Amount` double NOT NULL,
  `isDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `User` int(10) DEFAULT '0',
  `Edit_Date` datetime DEFAULT NULL,
  `TIN` varchar(50) DEFAULT NULL,
  `STORE` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_salesinvoice_contacts` (`SupplierID`),
  CONSTRAINT `FK_salesinvoice_contacts` FOREIGN KEY (`SupplierID`) REFERENCES `contacts` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='this table referes to stockin, stockin invoice';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.salesinvoicedetails
CREATE TABLE IF NOT EXISTS `salesinvoicedetails` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SI_ID` int(10) unsigned NOT NULL,
  `ItemID` int(10) unsigned NOT NULL,
  `UnitPrice` double NOT NULL,
  `QTY` int(10) DEFAULT NULL,
  `Discount` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_salesinvoicedetails_salesinvoice` (`SI_ID`),
  KEY `FK_salesinvoicedetails_item` (`ItemID`),
  CONSTRAINT `FK_salesinvoicedetails_item` FOREIGN KEY (`ItemID`) REFERENCES `item` (`ID`),
  CONSTRAINT `FK_salesinvoicedetails_salesinvoice` FOREIGN KEY (`SI_ID`) REFERENCES `salesinvoice` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='this table refers to stockin details, stockin invoice details';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.user
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(15) NOT NULL,
  `password` varchar(200) NOT NULL,
  `authentication` int(10) unsigned NOT NULL,
  `fname` varchar(50) DEFAULT NULL,
  `lname` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='0-sales\r\n1-cashier\r\n2-maintenance\r\n3-administrator';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.user_privileges
CREATE TABLE IF NOT EXISTS `user_privileges` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Role` int(10) DEFAULT NULL,
  `Inventory_Add` tinyint(1) DEFAULT '0',
  `Inventory_Edit` tinyint(1) DEFAULT '0',
  `Inventory_View` tinyint(1) DEFAULT '0',
  `Invoice_Add` tinyint(1) DEFAULT '0',
  `Invoice_Edit` tinyint(1) DEFAULT '0',
  `Invoice_View` tinyint(1) DEFAULT '0',
  `Receipt_Add` tinyint(1) DEFAULT '0',
  `Receipt_Edit` tinyint(1) DEFAULT '0',
  `Receipt_View` tinyint(1) DEFAULT '0',
  `Contacts` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='0-sales\r\n1-cashier\r\n2-maintenance\r\n3-administrator';

-- Data exporting was unselected.


-- Dumping structure for table jim_boltcenter.weight
CREATE TABLE IF NOT EXISTS `weight` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Measurement` varchar(10) DEFAULT NULL,
  `Converter` int(10) DEFAULT NULL,
  `Description` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.


-- Dumping structure for trigger jim_boltcenter.onitemupdate
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `onitemupdate` BEFORE UPDATE ON `item` FOR EACH ROW BEGIN
SET NEW.UpdateDate = NOW();
END//
DELIMITER ;
SET SQL_MODE=@OLD_SQL_MODE;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

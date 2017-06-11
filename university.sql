-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 11, 2017 at 10:59 PM
-- Server version: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `university`
--

-- --------------------------------------------------------

--
-- Table structure for table `1 1.1`
--

CREATE TABLE IF NOT EXISTS `1 1.1` (
  `student` text COLLATE utf8_unicode_ci,
  `1_midterm1` int(11) DEFAULT NULL,
  `1_midterm2` int(11) DEFAULT NULL,
  `1_exam` int(11) DEFAULT NULL,
  `3_midterm1` int(11) DEFAULT NULL,
  `3_midterm2` int(11) DEFAULT NULL,
  `3_exam` int(11) DEFAULT NULL,
  `4_midterm1` int(11) DEFAULT NULL,
  `4_midterm2` int(11) DEFAULT NULL,
  `4_exam` int(11) DEFAULT NULL,
  `2_pass` int(11) DEFAULT NULL,
  `5_pass` int(11) DEFAULT NULL,
  `6_pass` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `1 1.1`
--

INSERT INTO `1 1.1` (`student`, `1_midterm1`, `1_midterm2`, `1_exam`, `3_midterm1`, `3_midterm2`, `3_exam`, `4_midterm1`, `4_midterm2`, `4_exam`, `2_pass`, `5_pass`, `6_pass`) VALUES
('1', 4, NULL, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1),
('2', NULL, NULL, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `facultydb`
--

CREATE TABLE IF NOT EXISTS `facultydb` (
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Payment` decimal(10,0) NOT NULL,
  `Professions` text COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

--
-- Dumping data for table `facultydb`
--

INSERT INTO `facultydb` (`Name`, `ID`, `Payment`, `Professions`, `Description`) VALUES
('applied math', 1, '540000', ',1,2', 'math and stuff'),
('physics', 2, '540000', '', 'magic and shit'),
('Math', 3, '270000', ',3', 'Math and math and math and even more math'),
('test', 4, '1000', '', 'test');

-- --------------------------------------------------------

--
-- Table structure for table `groupdb`
--

CREATE TABLE IF NOT EXISTS `groupdb` (
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Monday` text COLLATE utf8_unicode_ci NOT NULL,
  `Thuesday` text COLLATE utf8_unicode_ci NOT NULL,
  `Wednesday` text COLLATE utf8_unicode_ci NOT NULL,
  `Thursday` text COLLATE utf8_unicode_ci NOT NULL,
  `Friday` text COLLATE utf8_unicode_ci NOT NULL,
  `Saturday` text COLLATE utf8_unicode_ci NOT NULL,
  `Elder` text COLLATE utf8_unicode_ci NOT NULL,
  `Students` text COLLATE utf8_unicode_ci NOT NULL,
  `Profession` int(11) NOT NULL,
  `Faculty` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=6 ;

--
-- Dumping data for table `groupdb`
--

INSERT INTO `groupdb` (`Name`, `ID`, `Monday`, `Thuesday`, `Wednesday`, `Thursday`, `Friday`, `Saturday`, `Elder`, `Students`, `Profession`, `Faculty`) VALUES
('6.2', 1, '1', '1', '1', '1', '1', '', '', ',1,2,18', 1, 1),
('6.1', 2, '1', '1', '1', '1', '1', '', '', ',17', 1, 1),
('math crazies', 3, '', '', '', '', '', '', '', '', 3, 3),
('602', 4, '', '', '', '', '', '', '', '', 2, 1),
('603', 5, '', '', '', '', '', '', '', '', 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `professiondb`
--

CREATE TABLE IF NOT EXISTS `professiondb` (
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Prof. Subj. 1.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 1.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 1.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 1.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 2.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 2.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 2.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 2.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 3.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 3.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 3.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 3.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 4.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 4.1` text COLLATE utf8_unicode_ci NOT NULL,
  `Prof. Subj. 4.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Non-Prof. Subj. 4.2` text COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci NOT NULL,
  `Groups` text COLLATE utf8_unicode_ci NOT NULL,
  `Faculty` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=4 ;

--
-- Dumping data for table `professiondb`
--

INSERT INTO `professiondb` (`Name`, `ID`, `Prof. Subj. 1.1`, `Non-Prof. Subj. 1.1`, `Prof. Subj. 1.2`, `Non-Prof. Subj. 1.2`, `Prof. Subj. 2.1`, `Non-Prof. Subj. 2.1`, `Prof. Subj. 2.2`, `Non-Prof. Subj. 2.2`, `Prof. Subj. 3.1`, `Non-Prof. Subj. 3.1`, `Prof. Subj. 3.2`, `Non-Prof. Subj. 3.2`, `Prof. Subj. 4.1`, `Non-Prof. Subj. 4.1`, `Prof. Subj. 4.2`, `Non-Prof. Subj. 4.2`, `Description`, `Groups`, `Faculty`) VALUES
('cyber security', 1, ',1,3,4', ',2,5,6', ',1,3,4', ',6', '', '', '', '', '', '', '', '', '', '', '', '', 'linus and shit', '', 1),
('applied math', 2, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'math', '', 1),
('Math', 3, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'more math', '', 3);

-- --------------------------------------------------------

--
-- Table structure for table `professordb`
--

CREATE TABLE IF NOT EXISTS `professordb` (
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Surname` text COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci NOT NULL,
  `Monday` text COLLATE utf8_unicode_ci NOT NULL,
  `Tuesday` text COLLATE utf8_unicode_ci NOT NULL,
  `Wednesday` text COLLATE utf8_unicode_ci NOT NULL,
  `Thursday` text COLLATE utf8_unicode_ci NOT NULL,
  `Friday` text COLLATE utf8_unicode_ci NOT NULL,
  `Saturday` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `professordb`
--

INSERT INTO `professordb` (`Name`, `ID`, `Surname`, `Description`, `Monday`, `Tuesday`, `Wednesday`, `Thursday`, `Friday`, `Saturday`) VALUES
('Levon', 1, 'Miqayelyan', 'math analiz', '', '', '', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `studentdb`
--

CREATE TABLE IF NOT EXISTS `studentdb` (
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  `Surname` text COLLATE utf8_unicode_ci NOT NULL,
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` text COLLATE utf8_unicode_ci NOT NULL,
  `Group` text COLLATE utf8_unicode_ci NOT NULL,
  `Profession` int(11) NOT NULL,
  `Faculty` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=19 ;

--
-- Dumping data for table `studentdb`
--

INSERT INTO `studentdb` (`Name`, `Surname`, `ID`, `Description`, `Group`, `Profession`, `Faculty`) VALUES
('areg', 'vrt', 1, '2000 may 12', '1', 1, 1),
('armen', 'manukyan', 2, '', '1', 1, 1),
('Samvel', 'Baghdasaryan', 3, 'Samo', '', 0, 0),
('Adolfik', 'Reyxstagyan', 17, 'meh', '2', 1, 1),
('Adolfik', 'Reyxstagyan', 18, 'meh meh', '1', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `subjectdb`
--

CREATE TABLE IF NOT EXISTS `subjectdb` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=8 ;

--
-- Dumping data for table `subjectdb`
--

INSERT INTO `subjectdb` (`ID`, `Name`) VALUES
(1, 'Programming'),
(2, 'Ecology'),
(3, 'Mathematical Analysis'),
(4, 'Discrete Math'),
(5, 'Armenian'),
(6, 'English'),
(7, 'Physics');

-- --------------------------------------------------------

--
-- Table structure for table `usersdb`
--

CREATE TABLE IF NOT EXISTS `usersdb` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` text COLLATE utf8_unicode_ci NOT NULL,
  `Password` text COLLATE utf8_unicode_ci NOT NULL,
  `Permission` int(11) NOT NULL,
  `studentID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=21 ;

--
-- Dumping data for table `usersdb`
--

INSERT INTO `usersdb` (`ID`, `Username`, `Password`, `Permission`, `studentID`) VALUES
(1, 'areg.vrt', 'UA3JGPzN', 0, 1),
(2, 'armen.manukyan', 'armen', 0, 2),
(3, 'admin', 'admin', 31, NULL),
(4, 'Levon.Miqayelyan', 'analiz', 19, NULL),
(5, 'Samvel.Baghdasaryan', 'Rj9rkHMx', 0, 3),
(19, 'Adolfik.Reyxstagyan', 'c0wGxrOP', 0, 17),
(20, 'Adolfik.Reyxstagyan', 'OamuJIpS', 0, 18);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

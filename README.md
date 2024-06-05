bu web programı bir üniverste içinde yapılan sosyal sorumluluk projeleri veya ödenek çıkartılan projelerin verisini üniversite bünyesinde tutulması için yapılmıştır. bu veriler mysql kullanılarak tutulmaktadır. sql şemamızın adı veriyonetimsistemi ve tabloların oluşturulması için gerekli komutlar ise : 

USE veriyonetimsistemi;

CREATE TABLE `projetablo` (

  `projeOneriTarihi` date NOT NULL,

  `projeAdi` varchar(45) NOT NULL,

  `projeTuru` varchar(45) NOT NULL,

  `projeDanismani` varchar(45) NOT NULL,

  `projeYurutucusu` varchar(45) NOT NULL,

  `projeEkibi` text NOT NULL,

  `projeKonusu` text NOT NULL,

  `projeAmaci` text NOT NULL,

  `projeHedefKitlesi` text NOT NULL,

  `projeSuresi` int NOT NULL,

  `projeButcesi` int NOT NULL,

  `projeEtkilikleri` text NOT NULL,

  `projeBaslangicTarihi` date NOT NULL,

  `projeBitisTarihi` date NOT NULL,

  `projeKurumKuruluslar` text NOT NULL,

  `projeMateryaller` text NOT NULL,

  `sistemeYukleyen` varchar(45) DEFAULT NULL,

  `sistemeYukleyenID` int DEFAULT NULL,

  `projeid` int NOT NULL AUTO_INCREMENT,

  PRIMARY KEY (`projeid`)

) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `user` (

  `userid` int NOT NULL,

  `userName` varchar(45) DEFAULT NULL,

  `userPassword` varchar(45) DEFAULT NULL,

  `userRole` varchar(45) DEFAULT NULL,

  `fullName` varchar(45) DEFAULT NULL,

  PRIMARY KEY (`userid`)

) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

bu web programı bir üniverste içinde yapılan sosyal sorumluluk projeleri veya ödenek çıkartılan projelerin verisini üniversite bünyesinde tutulması için yapılmıştır. bu veriler mysql kullanılarak tutulmaktadır. sql şemamızın adı veriyonetimsistemi ve tabloların oluşturulması için gerekli kodlar ise : 
CREATE TABLE `projetablo` (
  `projeAdi` varchar(45) DEFAULT NULL,
  `projeYapimcisi` varchar(45) DEFAULT NULL,
  `yapilanTarih` varchar(45) DEFAULT NULL,
  `projeSuresi` varchar(45) DEFAULT NULL,
  `projeButcesi` varchar(45) DEFAULT NULL,
  `sistemeYukleyen` varchar(45) DEFAULT NULL,
  `sistemeYukleyenID` int DEFAULT NULL,
  `projeid` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`projeid`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `user` (
  `userid` int NOT NULL,
  `userName` varchar(45) DEFAULT NULL,
  `userPassword` varchar(45) DEFAULT NULL,
  `userRole` varchar(45) DEFAULT NULL,
  `fullName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

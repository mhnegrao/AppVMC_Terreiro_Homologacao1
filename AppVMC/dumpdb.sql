-- --------------------------------------------------------
-- Servidor:                     db4free.net
-- Versão do servidor:           8.0.21 - MySQL Community Server - GPL
-- OS do Servidor:               Linux
-- HeidiSQL Versão:              11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Copiando estrutura para tabela tvmcdb.Clientes
DROP TABLE IF EXISTS `Clientes`;
CREATE TABLE IF NOT EXISTS `Clientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataInclusao` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Cancelado` tinyint(1) NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Celular` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LoginPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `EnderecoId` int DEFAULT NULL,
  `Observacao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Discriminator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsMensalista` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Clientes_EnderecoId` (`EnderecoId`),
  CONSTRAINT `FK_Clientes_Endereco_EnderecoId` FOREIGN KEY (`EnderecoId`) REFERENCES `Endereco` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.Endereco
DROP TABLE IF EXISTS `Endereco`;
CREATE TABLE IF NOT EXISTS `Endereco` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Cep` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Logradouro` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Municipio` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Bairro` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Uf` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Gps` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.FluxoCaixa
DROP TABLE IF EXISTS `FluxoCaixa`;
CREATE TABLE IF NOT EXISTS `FluxoCaixa` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataInclusao` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Cancelado` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.LancamentosCaixa
DROP TABLE IF EXISTS `LancamentosCaixa`;
CREATE TABLE IF NOT EXISTS `LancamentosCaixa` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataInclusao` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Cancelado` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.Mensalidades
DROP TABLE IF EXISTS `Mensalidades`;
CREATE TABLE IF NOT EXISTS `Mensalidades` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataInclusao` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Cancelado` tinyint(1) NOT NULL,
  `IdFiliado` int NOT NULL,
  `ValorMensalidade` decimal(65,30) NOT NULL,
  `DataVencimento` datetime(6) NOT NULL,
  `DataPagamento` datetime(6) NOT NULL,
  `Pago` tinyint(1) NOT NULL,
  `ValorPago` decimal(65,30) NOT NULL,
  `NomeAfiliado` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `AfiliadoId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Mensalidades_AfiliadoId` (`AfiliadoId`),
  CONSTRAINT `FK_Mensalidades_Clientes_AfiliadoId` FOREIGN KEY (`AfiliadoId`) REFERENCES `Clientes` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.Usuarios
DROP TABLE IF EXISTS `Usuarios`;
CREATE TABLE IF NOT EXISTS `Usuarios` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataInclusao` datetime(6) NOT NULL,
  `DataAlteracao` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Cancelado` tinyint(1) NOT NULL,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PasswordConfirm` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IdCategoria` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela tvmcdb.__EFMigrationsHistory
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Exportação de dados foi desmarcado.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

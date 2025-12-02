-- Script para inserir todas as licenças do Microsoft 365 no banco de dados
-- Execute este script após criar o banco de dados e rodar as migrations

USE projeto_banco;

-- Limpar tabela de licenças (opcional - use com cuidado em produção)
-- DELETE FROM Licencas;

-- Business
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (1, NOW()); -- BusinessBasic
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (2, NOW()); -- BusinessStandard
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (3, NOW()); -- BusinessPremium
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (4, NOW()); -- AppsForBusiness

-- Enterprise
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (10, NOW()); -- EnterpriseE1
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (11, NOW()); -- EnterpriseE3
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (12, NOW()); -- EnterpriseE5
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (13, NOW()); -- EnterpriseF3
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (14, NOW()); -- AppsForEnterprise

-- Education
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (20, NOW()); -- EducationA1
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (21, NOW()); -- EducationA3
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (22, NOW()); -- EducationA5

-- Perpétuas
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (30, NOW()); -- HomeAndBusiness2021
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (31, NOW()); -- ProfessionalPlus2021
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (32, NOW()); -- Standard2019

-- Serviços Específicos
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (40, NOW()); -- ProjectPlan3
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (41, NOW()); -- VisioPlan2
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (42, NOW()); -- ExchangeOnline
INSERT INTO Licencas (Tipo_Licenca, Data) VALUES (43, NOW()); -- PowerBIPro

-- Verificar inserção
SELECT * FROM Licencas ORDER BY Tipo_Licenca;

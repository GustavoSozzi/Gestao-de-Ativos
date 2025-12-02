-- Queries Úteis para Gestão de Ativos

-- ============================================
-- CONSULTAS DE USUÁRIOS
-- ============================================

-- Listar todos os usuários com suas licenças
SELECT 
    u.id_usuario,
    u.P_nome,
    u.Sobrenome,
    u.Matricula,
    u.Departamento,
    u.Cargo,
    u.Role,
    l.id_licenca,
    l.Tipo_Licenca,
    l.Data
FROM Usuario u
LEFT JOIN LicencaUsuario lu ON u.id_usuario = lu.UsuariosId_usuario
LEFT JOIN Licencas l ON lu.licencasId_Licenca = l.id_licenca
ORDER BY u.id_usuario, l.id_licenca;

-- Contar quantas licenças cada usuário possui
SELECT 
    u.id_usuario,
    u.P_nome,
    u.Sobrenome,
    COUNT(lu.licencasId_Licenca) as Total_Licencas
FROM Usuario u
LEFT JOIN LicencaUsuario lu ON u.id_usuario = lu.UsuariosId_usuario
GROUP BY u.id_usuario, u.P_nome, u.Sobrenome
ORDER BY Total_Licencas DESC;

-- Usuários sem licenças
SELECT 
    u.id_usuario,
    u.P_nome,
    u.Sobrenome,
    u.Departamento
FROM Usuario u
LEFT JOIN LicencaUsuario lu ON u.id_usuario = lu.UsuariosId_usuario
WHERE lu.licencasId_Licenca IS NULL;

-- ============================================
-- CONSULTAS DE ATIVOS
-- ============================================

-- Listar todos os ativos com usuário e localização
SELECT 
    a.id_ativo,
    a.Nome,
    a.Modelo,
    a.SerialNumber,
    a.CodInventario,
    a.Tipo,
    u.P_nome as Usuario_Nome,
    u.Sobrenome as Usuario_Sobrenome,
    u.Matricula,
    l.Cidade,
    l.Estado
FROM Ativos a
LEFT JOIN Usuario u ON a.id_usuario = u.id_usuario
INNER JOIN Localizacao l ON a.id_localizacao = l.id_localizacao
ORDER BY a.id_ativo;

-- Ativos por usuário
SELECT 
    u.P_nome,
    u.Sobrenome,
    u.Matricula,
    COUNT(a.id_ativo) as Total_Ativos
FROM Usuario u
LEFT JOIN Ativos a ON u.id_usuario = a.id_usuario
GROUP BY u.id_usuario, u.P_nome, u.Sobrenome, u.Matricula
ORDER BY Total_Ativos DESC;

-- Ativos sem usuário atribuído
SELECT 
    a.id_ativo,
    a.Nome,
    a.Modelo,
    a.SerialNumber,
    a.Tipo,
    l.Cidade,
    l.Estado
FROM Ativos a
INNER JOIN Localizacao l ON a.id_localizacao = l.id_localizacao
WHERE a.id_usuario IS NULL;

-- Ativos por localização
SELECT 
    l.Cidade,
    l.Estado,
    COUNT(a.id_ativo) as Total_Ativos
FROM Localizacao l
LEFT JOIN Ativos a ON l.id_localizacao = a.id_localizacao
GROUP BY l.id_localizacao, l.Cidade, l.Estado
ORDER BY Total_Ativos DESC;

-- ============================================
-- CONSULTAS DE CHAMADOS
-- ============================================

-- Listar todos os chamados com informações do ativo
SELECT 
    c.id_chamado,
    c.Titulo,
    c.Descricao,
    c.Status_Chamado,
    c.Data_Abertura,
    a.Nome as Ativo_Nome,
    a.Modelo as Ativo_Modelo,
    a.SerialNumber,
    u.P_nome as Usuario_Nome,
    u.Sobrenome as Usuario_Sobrenome
FROM Chamados c
INNER JOIN Ativos a ON c.id_Ativo = a.id_ativo
LEFT JOIN Usuario u ON a.id_usuario = u.id_usuario
ORDER BY c.Data_Abertura DESC;

-- Chamados por status
SELECT 
    Status_Chamado,
    COUNT(*) as Total
FROM Chamados
GROUP BY Status_Chamado;

-- Chamados abertos (pendentes)
SELECT 
    c.id_chamado,
    c.Titulo,
    c.Data_Abertura,
    a.Nome as Ativo,
    u.P_nome as Usuario
FROM Chamados c
INNER JOIN Ativos a ON c.id_Ativo = a.id_ativo
LEFT JOIN Usuario u ON a.id_usuario = u.id_usuario
WHERE c.Status_Chamado = 0  -- 0 = Aberto
ORDER BY c.Data_Abertura DESC;

-- ============================================
-- CONSULTAS DE LICENÇAS
-- ============================================

-- Licenças mais utilizadas
SELECT 
    l.Tipo_Licenca,
    COUNT(lu.UsuariosId_usuario) as Total_Usuarios
FROM Licencas l
LEFT JOIN LicencaUsuario lu ON l.id_licenca = lu.licencasId_Licenca
GROUP BY l.id_licenca, l.Tipo_Licenca
ORDER BY Total_Usuarios DESC;

-- Licenças não atribuídas
SELECT 
    l.id_licenca,
    l.Tipo_Licenca,
    l.Data
FROM Licencas l
LEFT JOIN LicencaUsuario lu ON l.id_licenca = lu.licencasId_Licenca
WHERE lu.UsuariosId_usuario IS NULL;

-- Usuários com licença específica (exemplo: EnterpriseE3 = 11)
SELECT 
    u.id_usuario,
    u.P_nome,
    u.Sobrenome,
    u.Departamento,
    l.Tipo_Licenca
FROM Usuario u
INNER JOIN LicencaUsuario lu ON u.id_usuario = lu.UsuariosId_usuario
INNER JOIN Licencas l ON lu.licencasId_Licenca = l.id_licenca
WHERE l.Tipo_Licenca = 11;

-- ============================================
-- CONSULTAS DE CONTRATOS
-- ============================================

-- Listar todos os contratos com informações do ativo
SELECT 
    ct.id_contrato,
    ct.Tipo,
    ct.Descricao,
    ct.Valor,
    a.Nome as Ativo_Nome,
    a.Modelo as Ativo_Modelo
FROM Contratos ct
INNER JOIN Ativos a ON ct.Id_Ativo = a.id_ativo
ORDER BY ct.Valor DESC;

-- Valor total de contratos por ativo
SELECT 
    a.id_ativo,
    a.Nome,
    a.Modelo,
    SUM(ct.Valor) as Valor_Total_Contratos
FROM Ativos a
LEFT JOIN Contratos ct ON a.id_ativo = ct.Id_Ativo
GROUP BY a.id_ativo, a.Nome, a.Modelo
HAVING Valor_Total_Contratos > 0
ORDER BY Valor_Total_Contratos DESC;

-- ============================================
-- RELATÓRIOS GERENCIAIS
-- ============================================

-- Dashboard: Resumo geral do sistema
SELECT 
    (SELECT COUNT(*) FROM Usuario) as Total_Usuarios,
    (SELECT COUNT(*) FROM Ativos) as Total_Ativos,
    (SELECT COUNT(*) FROM Licencas) as Total_Licencas,
    (SELECT COUNT(*) FROM Chamados WHERE Status_Chamado = 0) as Chamados_Abertos,
    (SELECT COUNT(*) FROM Contratos) as Total_Contratos,
    (SELECT SUM(Valor) FROM Contratos) as Valor_Total_Contratos;

-- Ativos por departamento
SELECT 
    u.Departamento,
    COUNT(a.id_ativo) as Total_Ativos
FROM Usuario u
LEFT JOIN Ativos a ON u.id_usuario = a.id_usuario
WHERE u.Departamento IS NOT NULL AND u.Departamento != ''
GROUP BY u.Departamento
ORDER BY Total_Ativos DESC;

-- Usuários por role
SELECT 
    Role,
    COUNT(*) as Total
FROM Usuario
GROUP BY Role;

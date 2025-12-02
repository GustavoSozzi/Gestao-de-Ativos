#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script para gerar SQL de inserção de 1000 usuários
"""

nomes = ['João', 'Maria', 'Pedro', 'Ana', 'Carlos', 'Juliana', 'Roberto', 'Fernanda', 'Lucas', 'Patricia',
         'Ricardo', 'Camila', 'Bruno', 'Amanda', 'Felipe', 'Beatriz', 'Thiago', 'Larissa', 'Gustavo', 'Isabela',
         'Rafael', 'Gabriela', 'Daniel', 'Mariana', 'Rodrigo', 'Aline', 'Marcelo', 'Vanessa', 'Paulo', 'Renata',
         'André', 'Tatiana', 'Fábio', 'Cristina', 'Leandro', 'Simone', 'Vinicius', 'Priscila', 'Henrique', 'Adriana',
         'Sérgio', 'Elaine', 'Márcio', 'Luciana', 'Alexandre', 'Mônica', 'Diego', 'Carla', 'Júlio', 'Sandra']

sobrenomes = ['Silva', 'Santos', 'Oliveira', 'Costa', 'Ferreira', 'Almeida', 'Lima', 'Rodrigues', 'Martins', 'Souza',
              'Pereira', 'Barbosa', 'Carvalho', 'Ribeiro', 'Gomes', 'Dias', 'Moreira', 'Araújo', 'Nascimento', 'Freitas',
              'Mendes', 'Castro', 'Rocha', 'Pinto', 'Azevedo', 'Cardoso', 'Teixeira', 'Correia', 'Monteiro', 'Nunes',
              'Vieira', 'Cunha', 'Melo', 'Farias', 'Batista', 'Ramos', 'Lopes', 'Fernandes', 'Cavalcanti', 'Moura',
              'Campos', 'Duarte', 'Reis', 'Barros', 'Santana', 'Macedo', 'Nogueira', 'Siqueira', 'Fonseca', 'Guimarães']

departamentos = ['TI', 'Financeiro', 'RH', 'Marketing', 'Vendas', 'Operações', 'Administrativo', 'Compras', 
                 'Qualidade', 'Logística', 'Projetos', 'Jurídico', 'Comercial', 'Produção', 'Manutenção']

cargos_ti = ['Analista de Sistemas', 'Desenvolvedor', 'Suporte Técnico', 'Coordenador de TI', 'DevOps', 
             'Analista de Segurança', 'Arquiteto de Software', 'Analista de Dados', 'Desenvolvedor Full Stack',
             'Analista de Infraestrutura', 'Scrum Master', 'Desenvolvedor Backend', 'Desenvolvedor Frontend',
             'Analista de Banco de Dados', 'Analista de Suporte', 'Engenheiro de Software', 'Analista de Redes']

cargos_outros = ['Contador', 'Gerente', 'Analista', 'Assistente', 'Coordenador', 'Supervisor', 'Comprador',
                 'Vendedor', 'Representante Comercial', 'Auditor', 'Recrutador', 'Tesoureiro', 'Secretária',
                 'Designer Gráfico', 'Analista de Mídias Sociais', 'Analista de Processos', 'Analista de Custos']

print("-- Script para inserir 1000 usuários no banco de dados")
print("-- Database: projeto_banco")
print()
print("USE projeto_banco;")
print()
print("INSERT INTO usuario (P_nome, Sobrenome, Matricula, Departamento, Cargo, UserIdentifier, Role, Password) VALUES")

lines = []
for i in range(1, 1001):
    matricula = 30000 + i
    nome = nomes[i % len(nomes)]
    sobrenome = sobrenomes[i % len(sobrenomes)]
    departamento = departamentos[i % len(departamentos)]
    
    if departamento == 'TI':
        cargo = cargos_ti[i % len(cargos_ti)]
    else:
        cargo = f"{cargos_outros[i % len(cargos_outros)]} de {departamento}"
    
    # A cada 50 usuários, criar um ADMIN
    role = 'ADMIN' if i % 50 == 0 else 'TEAM_MEMBER'
    
    line = f"('{nome}', '{sobrenome}', {matricula}, '{departamento}', '{cargo}', UUID(), '{role}', '$2a$11$hashed_password_{matricula}')"
    lines.append(line)

# Imprimir todos os registros
print(',\n'.join(lines) + ';')
print()
print("SELECT 'Usuários cadastrados com sucesso!' AS Status;")

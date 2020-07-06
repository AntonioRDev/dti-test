using ProvaDTI.Databases;
using ProvaDTI.Models;
using ProvaDTI.Util;
using System;
using System.Collections.Generic;

namespace ProvaDTI
{
    public class Clinic
    {
        public int DisplayMenu()
        {
            MainMenu();
            var input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    NewQueryMenu();
                    break;
                case 2:
                    SearchQueryMenu();
                    break;
                case 3:
                    SearchClientMenu();
                    break;
                case 9:
                    Console.WriteLine();
                    Console.WriteLine("Saindo...");
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Escolha inválida, por favor tente novamente.");
                    Console.WriteLine();
                    break;
            }


            return input;
        }

        private void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Clinica NutriDti");
            Console.WriteLine();
            Console.WriteLine("1. Nova consulta");
            Console.WriteLine("2. Buscar consulta");
            Console.WriteLine("3. Buscar cliente");
            Console.WriteLine("9. Sair");
            Console.WriteLine();
        }

        private void NewQueryMenu()
        {
            string userInput;

            Console.WriteLine();
            Console.WriteLine("Clinica NutriDti - Nova Consulta");
            Console.WriteLine();
            Console.WriteLine("1. Novo cliente");
            Console.WriteLine("2. Buscar cliente por nome");
            Console.WriteLine();

            userInput = CheckIntInputOption();
            var input = int.Parse(userInput);

            Client client = null;

            switch (input)
            {
                case 1:
                    client = NewClientMenu();
                    break;
                case 2:
                    client = SearchClientMenu();
                    if(client == null)
                    {
                        Console.WriteLine();
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Escolha inválida, por favor tente novamente.");
                    break;
            }

            var query = new Query();
            query.Id = FakeQueryDatabase.GetLength() + 1;
            query.Client = client;

            Console.WriteLine();
            Console.WriteLine("Digite o peso(Kg):");
            userInput = CheckDoubleInputOption();
            query.Weight = double.Parse(userInput);

            Console.WriteLine();
            Console.WriteLine("Digite a % de gordura corporal:");
            userInput = CheckDoubleInputOption();
            query.FatPercentage = double.Parse(userInput);

            Console.WriteLine();
            Console.WriteLine("Digite a sensação física:");
            query.FisicSensation = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Digite os dados de restrição alimentar:");
            query.FoodRestritions = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Digite a meta de consumo calórico para o paciente:");
            userInput = CheckDoubleInputOption();
            var caloricGoal = double.Parse(userInput);

            // Cálculo de dieta
            var foodGroups = FakeFoodsDatabase.DietCombination(caloricGoal);
            ShowDietsGroups(foodGroups);

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para finalizar a consulta");
            Console.ReadKey();

            FakeQueryDatabase.SaveQuery(query);
        }

        private void SearchQueryMenu()
        {
            string userInput;

            Console.WriteLine();
            Console.WriteLine("Clinica NutriDti - Buscar consulta");
            Console.WriteLine();
            Console.WriteLine("1. Buscar consulta por código de consulta");
            Console.WriteLine("2. Listar consultas");
            Console.WriteLine();

            userInput = CheckIntInputOption();
            var input = int.Parse(userInput);

            switch (input)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("Digite o código da consulta:");
                    Console.WriteLine();
                    var queryId = int.Parse(Console.ReadLine());
                    var query = FakeQueryDatabase.GetQueryById(queryId);

                    Console.WriteLine($"Consulta {query.Id} - {query.DateTime.ToShortDateString() + " - " + query.DateTime.ToShortTimeString()}");
                    Console.WriteLine($"Paciente: {query.Client.Name}");
                    Console.WriteLine($"Peso: {query.Weight}");
                    Console.WriteLine($"% de gordura corporal: {query.FatPercentage}");
                    Console.WriteLine($"Sensação física: {query.FisicSensation}");
                    Console.WriteLine($"Dados das restrições alimentares: {query.FoodRestritions}");

                    break;
                case 2:
                    var queries = FakeQueryDatabase.GetAllQueries();

                    foreach (var q in queries)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Consulta {q.Id} - {q.DateTime.ToShortDateString() + " - " + q.DateTime.ToShortTimeString()}");
                        Console.WriteLine($"Paciente: {q.Client.Name}");
                        Console.WriteLine($"Peso: {q.Weight}");
                        Console.WriteLine($"% de gordura corporal: {q.FatPercentage}");
                        Console.WriteLine($"Sensação física: {q.FisicSensation}");
                        Console.WriteLine($"Dados das restrições alimentares: {q.FoodRestritions}");
                        Console.WriteLine();
                    }

                    if(queries.Count == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nenhuma consulta encontrada");
                        Console.WriteLine();
                    }

                    break;
                default:
                    break;
            }
        }

        private Client SearchClientMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Digite o nome do cliente:");
            Console.WriteLine();
            var name = CheckStringInputOption();

            var client = FakeClientDatabase.GetClientByName(name);

            if (client == null)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Cliente não encontrado.");
                Console.WriteLine();
                return null;
            }

            Console.WriteLine();
            Console.WriteLine($"Nome: {client.Name}");
            Console.WriteLine($"Endereço: {client.Address}");
            Console.WriteLine($"Numero: {client.PhoneNumber}");
            Console.WriteLine($"Data de nascimento: {client.DateOfBirth}");
            Console.WriteLine();

            return client;
        }

        private Client NewClientMenu()
        {
            var client = new Client();
            Console.WriteLine();
            Console.WriteLine("Digite o nome:");
            client.Name = CheckStringInputOption();

            Console.WriteLine();
            Console.WriteLine("Digite endereço:");
            client.Address = CheckStringInputOption();

            Console.WriteLine();
            Console.WriteLine("Digite o email:");
            client.Email = CheckEmailInputOption();

            Console.WriteLine();
            Console.WriteLine("Digite a data de nascimento(dd/mm/YYYY):");
            client.DateOfBirth = CheckDateInputOption();

            FakeClientDatabase.SaveClient(client);

            return client;
        }

        private void ShowDietsGroups(List<List<Food>> dietGroups)
        {
            for(int i=0; i < dietGroups.Count; i++)
            {
                double totalCalories = 0;
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine($"Dieta {i+1}");

                foreach (var food in dietGroups[i])
                {
                    Console.WriteLine();
                    Console.WriteLine($"{food.Name}");
                    Console.WriteLine($"Grupo: {food.FoodGroup.ToString()}");
                    Console.WriteLine($"Quantidade calórica: {food.CaloricAmount} - Porção: {food.Portion}");
                    totalCalories += food.CaloricAmount;
                }

                Console.WriteLine();
                Console.WriteLine($"Total de calorias: {totalCalories}");
            }
        }

        private string CheckIntInputOption()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = Validator.IsValidIntegerConversion(userInput);

                if (!validInput)
                {
                    Console.WriteLine();
                    Console.WriteLine("Opção inválida, tente novamente uma opção válida");
                    Console.WriteLine();
                }
            } while (!validInput);

            return userInput;
        }

        private string CheckDoubleInputOption()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = Validator.IsValidDoubleConversion(userInput);

                if (!validInput)
                {
                    Console.WriteLine();
                    Console.WriteLine("Número inválido, tente novamente com um número válido");
                    Console.WriteLine();
                }
            } while (!validInput);

            return userInput;
        }

        private string CheckStringInputOption()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = !Validator.IsEmptyString(userInput);

                if (!validInput)
                {
                    Console.WriteLine("Esse campo não pode ser vazio!");
                    Console.WriteLine();
                }
            } while (!validInput);

            return userInput;
        }

        private string CheckDateInputOption()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = Validator.IsValidDate(userInput);

                if (!validInput)
                {
                    Console.WriteLine();
                    Console.WriteLine("Data inválida, tente novamente com uma data válida no padrão dd/MM/YYYY");
                    Console.WriteLine();
                }
            } while (!validInput);

            return userInput;
        }

        private string CheckEmailInputOption()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = Validator.IsValidEmail(userInput);

                if (!validInput)
                {
                    Console.WriteLine();
                    Console.WriteLine("Email inválido, tente novamente com um email válido");
                    Console.WriteLine();
                }
            } while (!validInput);

            return userInput;
        }
    }
}

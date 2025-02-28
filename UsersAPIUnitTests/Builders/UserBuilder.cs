﻿using UsersArbolusAPI.Models;

namespace UsersAPIUnitTests.Builders
{
    public class UserBuilder
    {
        private int id;
        private string? firstName;
        private string? lastName;
        private string? email;
        private int age;
        private string? gender;
        private string? country;
        private int height;
        private string? image;
        private string? favoriteColor;
        private string? favoriteFood;

        public UserBuilder WithSampleData()
        {
            id = 1;
            firstName = "Marcy";
            lastName = "Karadzas";
            email = "mkaradzas1@visualengin.com";
            age = 21;
            gender = Gender.Female;
            country = "Loompalandia";
            height = 99;
            image = "https://s3.eu-central-1.amazonaws.com/napptilus/level-test/1.jpg";
            favoriteColor = "red";
            favoriteFood = "Chocolat";

            return this;
        }

        public User Create() =>
            new ()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Gender = gender,
                Country = country,
                Height = height,
                Image = image,
                Profession = "Developer",
                Email = email,
                Favorite = new Favorite
                {
                    Color = favoriteColor,
                    Food = favoriteFood,
                    RandomString = "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",
                    Song = "Oompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do\\nOompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do\\nOompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do\\nOompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do\\nOompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do\\nOompa Loompas:\\nOompa Loompa doompadee doo\\nI've got another puzzle for you\\nOompa Loompa doompadah dee\\nIf you are wise you'll listen to me\\nWhat do you get from a glut of TV?\\nA pain in the neck and an IQ of three\\nWhy don't you try simply reading a book?\\nOr could you just not bear to look?\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no\\nYou'll get no commercials\\nOompa Loompa Doompadee Dah\\nIf you're like reading you will go far\\nYou will live in happiness too\\nLike the Oompa\\nOompa Loompa doompadee do",
                }
            };
    }
}

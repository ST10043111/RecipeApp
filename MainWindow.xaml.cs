using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;


namespace RecipeApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Recipe> recipes;
        private ChartValues<double> chartValues;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            recipes = new List<Recipe>();
            chartValues = new ChartValues<double>();
            pieChart.DataContext = this; // Set the data context for the chart
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void BtnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Get ingredient details from text boxes
            string ingredientName = txtIngredientName.Text.Trim();
            double ingredientQuantity;
            if (double.TryParse(txtIngredientQuantity.Text.Trim(), out ingredientQuantity))
            {
                // Parsing successful, continue with the logic
                double.Parse(txtIngredientQuantity.Text.Trim());
            }
            else
            {
                // Parsing failed, handle the invalid input
                MessageBox.Show("Invalid quantity value. Please enter a valid numeric value.");
                return; // or perform any other appropriate action
            }

            string ingredientUnit = txtIngredientUnit.Text.Trim();
            int ingredientCalories;
            if (int.TryParse(txtIngredientCalories.Text.Trim(), out ingredientCalories))
            {
                // Parsing successful, continue with the logic
                int.Parse(txtIngredientCalories.Text.Trim());
            }
            else
            {
               
                MessageBox.Show("Invalid calories value. Please enter a valid numeric value.");
                return; 
            }

            string ingredientFoodGroup = txtIngredientFoodGroup.Text.Trim();

           
            Ingredient ingredient = new Ingredient(ingredientName, ingredientQuantity, ingredientUnit, ingredientCalories, ingredientFoodGroup);

           
            lstIngredients.Items.Add(ingredient);

           
            txtIngredientName.Clear();
            txtIngredientQuantity.Clear();
            txtIngredientUnit.Clear();
            txtIngredientCalories.Clear();
            txtIngredientFoodGroup.Clear();

          
            int totalCalories = CalculateTotalCalories();
            MessageBox.Show($"Total Calories: {totalCalories}");
        }

        private int CalculateTotalCalories()
        {
            int totalCalories = 0;

            foreach (Ingredient ingredient in lstIngredients.Items)
            {
                totalCalories += (int)ingredient.Calories; 
            }

            return totalCalories;
        }


      
        public ChartValues<double> ChartValues
        {
            get { return chartValues; }
            set
            {
                chartValues = value;
                OnPropertyChanged(nameof(ChartValues));
            }
        }

        private void BtnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Get the recipe name from the text box
            string recipeName = txtRecipeName.Text.Trim();

            // Create a new Recipe instance with the provided recipe name
            Recipe recipe = new Recipe(recipeName);

            // Get the list of ingredients from the ListBox
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var item in lstIngredients.Items)
            {
                if (item is Ingredient ingredient)
                {
                    ingredients.Add(ingredient);
                }
            }

            // Set the ingredients and steps for the recipe
            recipe.Ingredients = ingredients;
            recipe.Steps = new List<string> { txtSteps.Text.Trim() };

            // Add the recipe to the list of recipes
            recipes.Add(recipe);

            // Add the recipe to the ListBox
            lstRecipes.Items.Add(recipe);
            chartValues.Add(recipe.Ingredients.Count); // Add the number of ingredients as a chart value

            // Clear the form for the next recipe
            txtRecipeName.Clear();
            lstIngredients.Items.Clear();
            txtSteps.Clear();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Clear the form when canceling
            txtRecipeName.Clear();
            lstIngredients.Items.Clear();
            txtSteps.Clear();
            chartValues.Clear(); // Clear the chart values
        }

        private void lstRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the selected recipe from the ListBox
            Recipe selectedRecipe = lstRecipes.SelectedItem as Recipe;

            // Display the selected recipe details or perform any other desired action
            if (selectedRecipe != null)
            {
                // Fill the recipe name field
                txtRecipeName.Text = selectedRecipe.Name;

                // Clear the existing ingredients in the ListBox
                lstIngredients.Items.Clear();

                // Add the ingredients of the selected recipe to the ListBox
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    lstIngredients.Items.Add(ingredient);
                }

                // Fill the steps field
                txtSteps.Text = string.Join(Environment.NewLine, selectedRecipe.Steps);
            }
        }
        private void lstIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the selected ingredient from the ListBox
            Ingredient selectedIngredient = lstIngredients.SelectedItem as Ingredient;

            // Display the selected ingredient details or perform any other desired action
            if (selectedIngredient != null)
            {
                // Fill the ingredient fields
                txtIngredientName.Text = selectedIngredient.Name;
                txtIngredientQuantity.Text = selectedIngredient.Quantity.ToString();
                txtIngredientUnit.Text = selectedIngredient.Unit;
                txtIngredientCalories.Text = selectedIngredient.Calories.ToString();
                txtIngredientFoodGroup.Text = selectedIngredient.FoodGroup;

                //Set background color of the main window
                this.Background =Brushes.LightBlue;
            }
        }

    }
}

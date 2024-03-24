
// Create instance of view model
using Linq;

SamplesViewModel vm = new();

// Call Sample Method
var result = vm.GetAllQuery();

// Display Results
vm.Display(result);
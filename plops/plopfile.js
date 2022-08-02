export default function (plop) {
	// create your generators here
	plop.setGenerator('basics', {
		description: 'this is a skeleton plopfile',
		prompts: [], // array of inquirer prompts
		actions: []  // array of actions
	});
    plop.setGenerator('component with props with one property', {
        description: 'Create a component',
        // User input prompts provided as arguments to the template
        prompts: [
          {
            // Raw text input
            type: 'input',
            // Variable name for this input
            name: 'name',
            // Prompt to display on command line
            message: 'What is your component name?',

            type: 'input',
            name: 'Prop1',
            type: 'type',
            name: 'Prop1Type'

          },
        ],
        actions: [
          {
            // Add a new file
            type: 'add',
            // Path for the new file
            path: 'src/components/{{pascalCase name}}.js',
            // Handlebars template used to generate content of new file
            templateFile: 'plop-templates/Component.js.hbs',
          },
        ],
      });
};
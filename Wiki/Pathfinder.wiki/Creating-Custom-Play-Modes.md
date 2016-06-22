Pathfinder gives you a number of options to customize your play style. One of those options, available through the [[Pathfinder Settings|Pathfinder-Settings]] window at the Space Center, lets you change Pathfinder's Play Mode. The mod comes with three Play Modes:  

**Default:** All of the templates and converters are available to you.  
**Simplified:** Simplified Mode reduces the number of resources that you need to keep track of, and correspondingly reduces the number of available templates. It's great for those just starting out with Pathfinder, or for those who want some complexity in their bases without being overwhelming.  
**Pristine:** Pristine Mode strips away all templates and converters to leave you with nice looking parts with animations. Pristine Mode is nice for those who just want to make bases with Pathfinder's parts but don't want the hassle of managing a series of resource production chains.  

The best thing about Pathfinder's Play Modes is that you can create Play Modes of your own! A Play Mode is really just a Module Manager patch file that tells KSP how to configure the parts along with a PATHFINDER_PLAY_MODE config node that provides a name and description for the Play Mode. PATHFINDER_PLAY_MODE also specifies some default values to use for settings like whether or not to require resources to inflate a module, and whether or not to check for a required skill when changing a configuration. These default values can of course be changed by the player.  

When you open the Play Modes window, Pathfinder will look in the Pathfinder/PlayModes folder for any text files or config files with the PATHFINDER_PLAY_MODE configuration node, including any Play Mode files that you create.  

To create your own Play Mode, start by making copies of Simplified Mode.txt and/or Pristine Mode.txt. If you're starting from scratch to, say, integrate Pathfinder with another base building mod, Pristine Mode is a good place to start since it has none of Pathfinder's templates or converters. Simplified Mode would be a place to start for savvy users who want to use Pathfinder's dynamic template system; in this case you'd want to set up the base modules to use custom template nodes (specified by templateNodes). You aren't required to store your custom templates in the Play Mode file, but Pathfinder ensures that at most, only one Play Mode file is active at any given time (you'll see a bunch of .txt files and one .cfg file).  

# PATHFINDER_PLAY_MODE  
Here is a sample PlayMode:  
> PATHFINDER_PLAYMODE  
> {  
> 	//Name of the play mode  
> 	name = Simplified Mode  

> 	//Description  
> 	description = Simplified Mode reduces the number of resources required by Pathfinder, and reduces the available templates. This is great for players new to Pathfinder or for those that don't want the complexity of Default Mode. If you have Extraplanetary Launchpads installed, then you can produce RocketParts from Ore in the Hacienda's ISRU configuration.  
  
> 	//These are the default settings to use for the Pathfinder menu. The user is free to change them.  
> 	payToRemodel = false  
> 	requireSkillCheck = false  
> 	repairsRequireResources = false  
> 	partsCanBreak = false  
> }
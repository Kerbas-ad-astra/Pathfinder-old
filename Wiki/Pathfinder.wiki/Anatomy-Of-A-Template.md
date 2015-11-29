Pathfinder has the ability to dynamically change a part's resources and functionality while the game is running. For instance, the Ponderosa can be configured as a geology lab, an OSE Workshop, a habitat, and more. This is accomplished through templates that are parsed using custom part modules. If you want to create your own template, it's fairly easy to do:

First, open a text editor and load up a config file from a part whose functionality you like. For instance, let's say you like the longitude and latitude readings from the stock surface scanner. By looking at the OrbitalScanner.cfg file, you'll find:

MODULE  
{  
    name = ModuleGPS  
}

Make a copy of the module definition, you'll need it later.

Next, copy one of the Pathfinder templates found in Pathfinder/Templates, or use the sample template below. Be sure to keep the "PATHFINDER" config node name, as that's used by the Ponderosa to read the template.

Finally, paste the above MODULE code into the new config file, and save it. The next time you start KSP, your new template will be available.

To create a logo panel decal, [download the LogoTemplate from here](http://www.spellflight.com/KSP/LogoTemplate.psd). it consists of three layers: the background, GlowPanelBackground, and MaxDimensions layer. So long as your custom image appears within the circle defined by MaxDimensions, your image will show up file. Be sure to hide the MaxDimensions layer before saving the image. You can use any format you like that's recognized by KSP.

Sample template with comments:  
PATHFINDER  
{  
  //Optional. Name of the author who wrote the template  
  author = Angel-125

  //Optional. name of the template. Recommended if you write ModuleManager patches for the template.  
  name = GeoLab

  //Required. The title of the template  
  title = Pathfinder Geology Lab

  //Required. A shortened version of the title. Used internally.  
  shortName = Geology Lab

  //Required. A description of the template. This will show up in the Operations Manager  
  description = When you're out performing geoscience, it's nice to have a lab where you can analyze the results. That's where the Pathfinder Geology Lab comes in. With the right staff, you can make an impact in the field of astro geology.

  //Optional. If the template adjusts the mass of the Ponderosa module, then set the mass in metric tons here.  
  mass = 2.25

  //Optional. Specifies the amount of RocketParts needed to reconfigure the Ponderosa to the new template.  
  rocketParts = 900

  //Optional. The skill required to reconfigure the Ponderosa to the new template.  
  reconfigureSkill = Engineer

  //Required. The 256x256 image to show on the Ponderosa's logo panels. You can place the image anywhere you like as long as you specify the path here.  
  logoPanel = WildBlueIndustries/Pathfinder/Decals/Pathfinder

  //Required. The 256x256 specular image to show on the Ponderosa's logo panels. You can place the image anywhere you like as long as you specify the path here. 
  glowPanel = WildBlueIndustries/Pathfinder/Decals/PathfinderGlow

  //Optional. The tooltip to show the first time that the player inflates a Ponderosa with the template.  
  toolTip = You might want to staff your Geology Labs with scientists to make a lasting impact on geoscience.   Or profit from it... Just be sure you choose the right scientists for the job...

  //The title of the tooltip.  
  toolTipTitle = Your First Pathfinder Lab!

  //From here, you can add a variety of MODULE nodes and RESOURCE nodes that the Ponderosa will add. you can pick these from stock parts and from other mods.  
  
MODULE  
{  
    name = ModuleGPS  
}

MODULE  
{  
    name=ModuleBiomeScanner  
}

MODULE  
{  
    name = WBIGeologyLab  
}

RESOURCE  
{  
    name = ElectricCharge  
    amount = 200  
    maxAmount = 200  
    isTweakable = false  
}

}
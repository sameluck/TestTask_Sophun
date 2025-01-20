My solution works by Instantiating an adressable leaderboard asset through the PopupManagerServiceService, then populating the content panel in the leaderboard asset with prefabs of a player entry consisting of a background that changes color and size based on the player tier, player icon that gets downloaded from the internet and 2 text fields with name and score respectively. The information passed to the populating class is taken from the JSON file in the resources directory.

Design choices made: size adjustment of the entries is defined by their height, possibly not matching colors but ones that are most representative of the "tier"

changed the PopupManagerServiceService to have a root node passed to it, this allows for easier management of where we want things to spawn in the hierarchy, previously the objects could only be spawned at the top and had to be manualy inserted into their place via code.

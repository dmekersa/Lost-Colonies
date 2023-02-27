LA TECHNO
=========
C#/Monogame

LE JEU
======
On explore l'infini (Elite, Millennium 2.2, FTL).
- Génération procédurale de planètes (noms, type, ressources, etc.) dans des secteurs/galaxies.
https://wiki.alioth.net/index.php/Classic_Elite_planet_descriptions
On a des biotopes/types de planêtes (sable/Dune, Stargate, No man sky).
- Gaz
- Sable
- Organique
- Glace
- Lave
On survole la surface (Frontier FE) à la recherche de...
On explore des Donjons : Colonies sousterraines (Fallout, Cogmind, Desktop Dungeon)
On a des robots complémentaires (Codmind, Quadralien).
- Combat
- Hacker
- Tank
On des puzzles (Sokoban, Chip's Challenge, Link Awakening).
Le décor est destructible (Forget Me Not, Broforce)
On a des monstres en fonction du biotope.
On sauve des gens (Alien Syndrome).
Le monde est généré prodécuralement.
On peut s'échanger des clés. Une clé = un monde.

LES INSPIRATIONS MEMBRES
========================
Dome Keeper
Neurovoider
Void Expanse
Metal Mind
Dungeon of the Endless

Tour par tour
=============
** Rogue's Tale
Cogmind
Fallout Classic
XCOM
Space Hulk Tactic
Mario et Lapin Crétin
Final Fantasy Tactic
Advance War
Jagged Alliance
Phoenix point
Phantom Doctrine
Shining Force
** https://archrebel.blogspot.com/ (fantastique effet de grenade)

---
#Devlog : 

##Episode 1 (17/10/22):
Debrief, feature list, inspirations

##Episode 2 (24/10/22):
On a importé et adapté le framework du projet de ma LD36.
Ajouté un Service Locator.
Créé 2 scène : menu et test.

##Episode 3 (31/10/22):
Générateur de noms de planètes.
"..LEXEGEZACEBISOUSESARMAINDIREA.ERATENBERALAVETIEDORQUANTEISRION"
https://github.com/fragglet/txtelite/blob/master/txtelite.c

##Episode 4 (7/11/22)
Générateur de galaxie
Scene d'affichage de la galaxie avec le sélecteur de planète

##Episode 5 (14/11/22)
Chemin de fer

##Episode 6 (21/11/22)
Migrer le Control Manager au niveau du GameState
Splash Screen
Menu et musique (affichage synchro avec la musique)

##Episode 7 (28/11/22)
Cinématique de sortie du vaisseau mère
Création d'un GameState avec le générateur de galaxie
Uniformiser les contrôles (au lieu que chaque scène ait les siens)

##Episode 8 (05/12/22)
AssetManager

##Episode 9 (12/12/22)
Tweening

##Episode 10 (02/01/23)
Roguelike : on commence
Création d'une classe SurfaceMap pour représenter une surface de planète et affichage de cette surface dans la scène SceneSurface avec un tileset maison exporté de TIC-80.

##Episode 11 (09/01/23)
Surface : déplacement à la surface de la planète (débat : façon Starcraft ou pas)
- Mettre en place un offset
- Scroll par les bords, comme dans un RTS

##Episode 12 (16/01/23)
Surface
- Matérialiser un vaisseau
- Clic = le vaisseau rejoint la case cliquée

##Episode 13 (23/01/23)
- Curseur sur la map qui clignotte

##Episode 14 (30/01/23)
- Brouillard de guerre (fog of war)

##Episode 15 (06/02/23)
- Precalculer la surface de toutes les planêtes

##Episode 16 (13/02/23)
- Minimap

##Episode 17 (20/02/23)
- Placer une base
- Action : Land
- Commencer l'implémentation des donjons : Quel algo de génération ? 
  Binding of Isaac avec des salles de tailles variables, vue façon Rogue64, minimap façon Roguecraft
- Générer une surface plus réaliste (eau / terre / ...)
- Effet de warp


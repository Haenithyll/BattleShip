# BattleShip

## Objectifs :

### Objectifs principaux 
- Mettre en place un menu permettant au joueur de lancer une partie ou quitter le jeu. 
- Mettre en place une IA qui tire aléatoirement sur la grille de jeu en suivant un pattern de ratissage de la grille et tire autour des tirs réussis pour finir un bateau allié.
- Mettre en place deux grilles de jeu, une grille de tir (ennemie) et une grille alliée toute deux intéractibles et faites de boutons.
- Mettre en place des icones de navires renseignant sur l'état actuel des bateaux alliés comme ennemis.
- Mettre en place un évènement de fin de partie lorsqu'un joueur a perdu tout ses bateaux.

### Objectifs secondaires
- Mettre en place un système de bonus rajoutant des éléments de gameplay au jeu.
- Mettre en place une IA à difficulté modulable (facile, moyen, dur)
- Mettre en place un mode multijoueur 

## Avancée actuelle (18/02/2022) :
- Un menu a été mis en place et permet au joueur de lancer une partie ou quitter le jeu
- Une grille est générée au lancement de la partie => voir pour faire une première ohase de sélection des coordonnées des navires alliés puis la partie commence contre l'IA

## Commentaires :
- La prise en main de WPF a été plus compliquée qu'initialement attendu. Le style d'intéraction étant différent de celui du logiciel de développement Unity, il a fallu un temps d'adaptation et beaucoup de recherche afin de maîtriser les différents éléments, surtout graphiques, comme les Grid.
- Nous avons pris le temps de soigner nos différentes classes afin de rendre le tout très dynamique et laissant une liberté non négligeable à des modifications pouvant être apportées par un Game Designer (Emplacement des différents éléments de l'UI, taille de la grille, nombre de lignes, colonnes, nombres et types de navires...)

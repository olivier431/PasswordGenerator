# 420-1SS-SW: Développement: Sujets Spéciaux

## Projet : Gestionnaire de mots de passe

### Version #1

## Correction

1. Structure générale du projet (dépôt Git, solution, projets de bibliothèque de
   classes, application console, ...)
    - **note** : 3/3
2. Qualité du code (organisation du code, utilisation de méthodes appropriées,
   gestion des exceptions et des erreurs, ...)
    - **note** : 2/5
    - il ne devrait pas y avoir de code qui fait référence à la console dans la
      biblio de classes parce que ce code n'est pas réutilisable dans une
      application avec interface graphique par exemple
    - pourquoi ton `Main` est un `async Task` ?
    - *.NETCoreApp,Version=v6.0.AssemblyAttributes.cs(4, 12): [CS0579]
      Attribut 'global::System.Runtime.Versioning.TargetFrameworkAttribute' en
      double* lorsque j'essai d'exécuter to `Main`
    - je viens de voir que ton projet d'app console est dans le même dossier que
      la solution au lieu d'être dans un sous dossier, c'est peut-être ce qui
      cause des problèmes à l'exécution; tu ne devrais jamais cocher l'option de
      mettre tout dans le même dossier lors de la création d'une nouvelle
      solution, sauf pour les projets les plus simples
    - j'ai commenté le contenu de `PasswordGenerator.AssemblyInfo.cs` et
      maintenant ça a l'air de fonctionner
3. Tests unitaires (pour les classes/méthodes dans la bibliothèque de classes)
    - **note** : 0/5
4. Génération d'un mot de passe
    - **note** : 2/2
5. Gestion du fichier de mots de passe (créer, ouvrir, enregistrer, lister)
    - **note** : 4/4
6. Chercher un mot de passe
    - **note** : 3/3
7. Sélectionner un mot de passe (déchiffrer, cacher, mettre à jour, effacer)
    - **note** : 8/8

### Total : 22/30

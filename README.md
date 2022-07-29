# Blum-Entertainment

Rozgrywka:
Gra składa się z czterech rund. 
Celem gracza jest wykonanie zadania wyświelonego w oknie startowym rundy (zdobycie odpowiedniej ilości złota oraz zabicie określonej liczby przeciwników).
Po zabiciu przeciwnika istnieje 50 % szans na to że wyleci z niego losowy przedmiot, czyli shuriken lub melon.
W momencie gdy gracz podniesie shuriken, na ekranie pojawia się czerwony celownik który określa kierunek rzutu.
Gracz może podnieść tylko jeden shuriken. Aby podnieść kolejny, najpierw musi wyrzucić aktualny.
Mechanika shurikenów została dodana ze względu na trudność pokonania grzybów za pomocą miecza.
Melony przywracają jeden punkt zdrowia, tylko w momencie gdy gracz jest zraniony.
W pierwszej rundzie celowo na mapie zostały rozmieszczone shurikeny oraz melony, aby z łatwością można było przestestować mechaniki gry.

Sterowanie:
-ruch gracza (Klawiatura: WSAD) (Gamepad: Lewa gałka)
-skok (Klawiatura: Space) (Gamepad: Dolny przycisk - w przypadku Xboxa Przycisk A)
-atak mieczem (Klawiatura: L) (Gamepad: Prawy przycisk - w przypadku Xboxa Przycisk B)
-rzut shurikenem (Lewy przycisk myszy) (Gamepad: Prawy trigger - w przypadku Xboxa RT)
-celownik (mysz lub Prawa gałka na padzie)

Dodatkowe mechaniki:
-system rund
-system audio
-drop z przeciwników
-rzucanie shurikenem
-regeneracja zdrowia (melony)
-liczniki (złota, pokonanych przeciwników)
-spawnowanie przeciwników
-obsługa kursora za pomocą Gamepada

Wykorzystane wzorce projektowe:
-Finite State Machine (stany enemy, stany gracza)
-Object Pooling (spawnowanie enemy, spawnowanie coinów)
-Singleton (system rund, system audio)

Projekt został wykonany w oparciu o New Input System.

Wykorzystane assety:
https://kenney.nl/assets/ui-pack
https://assetstore.unity.com/packages/p/pixel-adventure-1-155360
https://opengameart.org/content/64-crosshairs-pack-split
https://freesound.org/
https://github.com/h8man/NavMeshPlus

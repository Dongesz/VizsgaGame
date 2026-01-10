#############################
# CastL Tower Defense Project - Refactor & Development Notes
# Generated: 2025-07-06
#############################

== ÁLTALÁNOS MEGJEGYZÉS ==
Nagyon stabil szerkezet, jól tagolt Singleton minták, világos felelősségek. A refactor és bővítés célja inkább a finomhangolás, nem a "megjavítás". Mindenhol törekedtem konkrétumokra.

-----------------------------------
== Scriptenkénti és modulonkénti megjegyzések ==
-----------------------------------

--- AudioManager.cs ---
✔ Jól elkülönített hangerő és SFX kezelés.
⚠ Esetleg: AudioClip enum vagy Dictionary (pl. "WinSound") hogy ne Inspector index alapján kelljen keresni.
⚠ Később: Zene kezelése (loop, fade in/out).
⚠ Jelenleg globális hangerő csak egy csúszkára van kötve – lehetne master/FX külön.

--- BasicBullet.cs ---
✔ Letisztult lövedék script.
⚠ A DestroyAfterTime coroutine delay-3 fix szám nem teljesen átlátható – érdemes kommenttel jelölni miért pont annyi.
⚠ Célszerű lehet a `SetDmg` metódus nagybetűvel (`SetDamage`) egységesség miatt.
⚠ Ha többféle bullet effekt kell (pl. robbanás), lehet `virtual void OnHit()` metódus, override-olható.

--- BaseTurret.cs ---
✔ Stabil targeting és shooting logika.
⚠ Jelenleg első találatot céloz random sorrendben. Később lehetne:
    • Legközelebbi
    • Legkevesebb HP
    • Random
⚠ A bps (bullet per second) paramétert akár inspector sliderrel limitálhatnád (0.1–5).
⚠ Ha több toronyfajta lesz, érdemes lehet `BaseTurret`-ből örökíteni specializáltakat.

--- BuildManager.cs ---
✔ Korrekt toronyválasztó.
⚠ Typo: `SelctedTower` -> `SelectedTower`.
⚠ Később index helyett property (`CurrentTower`) vagy enum típusú selection.

--- CooldownBarAnimator.cs ---
✔ Jó coroutine-alapú animáció.
⚠ A `finished` flag érdemes lehet event-té (OnCooldownComplete), így más script nem pollolná Update-ben.
⚠ A loop végtelen, amíg a wave aktív. Ha csak egyszeri animációt szeretnél, állítsd le magát a coroutine-t.

--- DataBaseManager.cs ---
✔ Alap login / scoreboard működik.
⚠ Nagyon fontos: jelszó jelenleg plain text – bcrypt mindenképp.
⚠ UI és DB logika szétválasztása (pl. külön LoginController).
⚠ Connection string ne legyen beégetve hosszú távon.
⚠ A TryLogin két SELECT-et egyszerre futtat (multi result set) – ez jó, csak dokumentáld.

--- Enemy.cs ---
✔ Tiszta adatmodell.
⚠ Később lehet ScriptableObject-re váltani, így inspectorból szerkeszthető lesz assetként.

--- EnemyBehaviour.cs ---
✔ Letisztult base class, jól virtualizálva.
⚠ `Die()` és `ReachGoal()` bővíthető animáció triggereléssel.
⚠ Később Event System használata (OnDeath, OnReachGoal) laza couplinghoz.

--- EnemyManager.cs ---
✔ Minimális global tracker.
⚠ Ha sok enemy lesz, lehet pooling és egy `List<GameObject>`.
⚠ A `DestroyAllEnemies()` robbanásszerű takarításhoz oké, de nem optimalizált.

--- EnemyMovement.cs ---
✔ Szép path following.
⚠ Nincs védelem üres path esetén.
⚠ Később lehet smoothing (Lerp) vagy sebesség-változtatás.
⚠ Megfontolható rigidbody helyett transform.position mozgatás, ha nincs fizikára szükség.

--- GameBuyMenu.cs ---
✔ Minden UI frissítés korrekt.
⚠ Az `OnGUI()` helyett Update() javasolt.
⚠ SpriteSwitch method bővíthető több sprite állapotra.
⚠ Animátor paraméterek string helyett hash (Animator.StringToHash).

--- GameManager.cs ---
✔ Letisztult win/lose logika.
⚠ `LevelDataHolder.Instance.currentlevel.text` string összehasonlítás helyett int parse jobb lenne.
⚠ Reset előtt a save frissítése explicit dokumentálva legyen.
⚠ Később event alapú győzelem-vereség triggerelés.

--- LevelDataHolder.cs ---
✔ Tiszta data holder singleton.
⚠ Inspectorban kötelező minden mezőt kitölteni, különben nullref lesz.
⚠ Ha több szint lesz, talán ScriptableObject + SceneData asset lehetne.

--- MainMenu.cs ---
✔ Reset és kilépés logika jól elkülönített.
⚠ A `FindObjectOfType<PlotManager>()` egyszer menthető mezőbe.
⚠ `FullReset()` esetleg coroutine lehet, ha sok dolgot törölsz frame-en belül.

--- MapBuilder.cs ---
✔ Szint inicializáció pontos.
⚠ Ha hiányzik "Path" vagy "End point", hibát dob.
⚠ A `plotsInLevel` static list helyett instance mező is lehetne, ha több MapBuilder létezne.

--- PlayerStatsManager.cs ---
✔ Letisztult állapotkezelés.
⚠ Később property és event alapú frissítés UI szinkronra.

--- PlotManager.cs ---
✔ Plot reset logika korrekt.
⚠ A `ResetAllPlots()` során `plotObj.GetComponent<PlotManager>()` hívás helyett lehetne a listában a PlotManager-t eltárolni.
⚠ `towerBuilder`-t nulláznál, de nem destroy-ol tornyot. Ez tudatos?

--- PlotUI.cs ---
✔ Hover és vizuális visszajelzés tiszta.
⚠ Ha később több állapot lesz (builded, disabled), enum használható.
⚠ `ResetPlotColor()` plusz paraméterrel bővíthető (pl. szín).

--- ProfileManager.cs ---
✔ Profil adatok frissítése korrekt.
⚠ Ha sok adatmező lesz, egy struct-ból töltés átláthatóbb.

--- ScoreBoardEntry.cs ---
✔ DTO osztály, nincs tennivaló.

--- SlimeEnemy.cs ---
✔ Shell class, későbbi bővítéshez.
⚠ Jelenleg semmit nem override-ol, nincs szerepe.

--- SpriteAnimator.cs ---
✔ Sprite animation tökéletes.
⚠ Ha több anim state lesz, enum és dictionary javasolt.
⚠ `AdjustSpriteDirection()` bővíthető felfelé-lefelé nézéssel.

--- StoneMine.cs ---
✔ Cooldown és reward logika jó.
⚠ Ha többféle bányászat lesz, `rewardAmount` paraméterezhető legyen.
⚠ Később Event alapú cooldown end trigger.

--- SwipeController.cs ---
✔ Carousel lapozás korrekt.
⚠ `LeanTween` függőség miatt dokumentáld, hogy kell plugin.
⚠ `currentPage` property-ből olvasható lehet.

--- TimerManager.cs ---
✔ Game időzítés jól megoldva.
⚠ `isFastForward` funkció implementálása hiányzik, csak flag van.
⚠ Később Time.timeScale integráció.

--- TimerUI.cs ---
✔ UI frissítés pontos.
⚠ Ha `TimerManager.Instance` nincs, `NullReference` lesz – védelem megfontolandó.

--- Tower.cs ---
✔ DTO, hibamentes.

--- TowerBuilder.cs ---
✔ Építés logika korrekt.
⚠ `SelctedTower` typo.
⚠ `TryBuildTower()` esetleg eventtel visszajelezhet UI-nek, ha nincs pénz.
⚠ Később build preview ghost object.

--- TowerManager.cs ---
✔ DestroyAllTowers funkció működik.
⚠ Ha poolingot használsz, Destroy helyett recycle.

--- WaveManager.cs ---
✔ Hullámkezelés jól összerakva.
⚠ `WinWave()` folyamatosan logolhat, Update helyett event trigger kéne.
⚠ Később Wave ScriptableObject vagy config JSON a hullámokhoz.

-----------------------------------

== VÉGSŐ JAVASLAT ==
Ha a következő refactort elkezded:
- Először naming consistency (SelctedTower stb).
- Másodszor OnGUI->Update.
- Harmadszor event-based kommunikáció a flag polling helyett.
- Negyedszer hosszú távon ScriptableObject assetek a hullámokhoz, tornyokhoz, ellenfelekhez.

Ez a projekt **nagyon szép kiindulási alap** – bátran told tovább!

###################################

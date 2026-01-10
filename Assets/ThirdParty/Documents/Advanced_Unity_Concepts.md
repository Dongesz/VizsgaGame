#############################
# Advanced Unity Concepts & Best Practices
# Generated: 2025-07-06
#############################

Az alábbi fogalmakat a profi Unity projektek rendszeresen használják.
Ezek közül egyiket sem kötelező azonnal bevezetned, de hosszú távon sokat segíthetnek a skálázhatóságban és tiszta architektúrában.

-----------------------------------
== SCRIPTABLE OBJECTS ==
- Adatok tárolására (pl. Tower statok, Enemy statok, Wave config).
- Előnye: Inspectorban assetként szerkeszthetők.
- Referenciaként hivatkozhatsz rá prefabokon.
- Példa: 
  • TowerData : ScriptableObject
  • EnemyData : ScriptableObject
  • WaveConfig : ScriptableObject

-----------------------------------
== EVENT-DRIVEN ARCHITECTURE ==
- Egységek közvetlen hívogatása helyett Event-eket lőnek.
- Segít laza couplingot tartani.
- UnityEvent, C# event, vagy akár saját EventBus.
- Példa:
  • OnEnemyDied
  • OnWaveStarted
  • OnPlayerHealthChanged

-----------------------------------
== OBJECT POOLING ==
- Gyakori instantiation/destroy helyett újrahasznosítod a példányokat.
- Különösen Bullet, Enemy esetében.
- Előnye: minimalizálja a GC (Garbage Collector) terhelést.
- Unity 2021+ verziókban van Pooling API (pl. ObjectPool<T>).

-----------------------------------
== DEPENDENCY INJECTION ==
- Singletonok helyett egy központi Container adja a referenciákat.
- Pl. Zenject vagy saját mini DI rendszer.
- Professzionális projekteknél nagyon elterjedt.

-----------------------------------
== ADDRESSABLE ASSETS ==
- Asset kezelés optimalizálására.
- Könnyebb letöltés, verziókezelés, memóriagazdálkodás.
- Jellemző nagy projektekben.

-----------------------------------
== STATE MACHINE ARCHITECTURE ==
- Pl. Enemy AI vagy Game State kezelésre.
- Könnyebb új állapotok (Attack, Flee, Patrol) hozzáadása.
- Implementálható:
  • Switch-case
  • State Pattern OOP
  • Unity Animator (pl. Animator Controller állapotok)

-----------------------------------
== UNITY JOBS / ECS (ENTITIES) ==
- Nagyon nagy számú entitás (több ezer) kezelésére.
- Alacsony szintű optimalizáció.
- Csak ha igazán nagy scale-t akarsz.

-----------------------------------
== SERIALIZATION & SAVE SYSTEM ==
- PlayerPrefs helyett fájl alapú mentés.
- JSON / Binary Formatter.
- Pl. SaveGameManager osztály.

-----------------------------------
== CUSTOM INSPECTORS & EDITOR TOOLS ==
- Saját editor GUI (pl. hullám szerkesztő).
- ScriptableObject assetek könnyebb konfigurálása.

###################################
Ez a lista inspirációként szolgál a jövőbeli refactorhoz és továbbfejlesztéshez.
Nem szükséges mind egyszerre bevezetni.
###################################

# ZE.FeaturedAppScope
```bash
https://github.com/ZapilinEntertainment/ZE.FeaturedScopes.git#main
```

# IFeatureModule
 Каждая фича задается своим объектом-модулем, в котором указываются зависимости и ресурсы. Весь функционал определяется добавленными на модуль интерфейсами.
 
 При этом все объекты приложения содержатся в трех скоупах:
 - AppScope - корневой
 - SessionScope - данные приложения
 - SceneScope - данные текущей игровой сцены / уровня
 Модуль может внедрять зависимости в любой из трех скоупов, а тажке грузить ресурсы при создании дочерних скоупов.
 
 Последовательность исполнения выглядит так: *AppScope* -> SessionAsyncEntryPoint -> *SessionScope* -> SсeneAsyncEntryPoint -> *SceneScope*. AsyncEntryPoint занимаются асинхронной загрузкой ресурсов перед тем как создать новый дочерний скоуп (если хоть один модуль реализует ISessionAsyncResourceLoader / ISceneAsyncResourceLoader).
 Дочерние скоупы (session\scene) не создаются сами по себе, вам придется писать логику, которая будет вызывать их стартовый asyncPoint.
 
 ## Запускаем
 - создаем scriptable object FeaturesModulesList
 - добавляем на стартовую сцену AppScope, указываем в нем FeaturesModulesList
 -  Создайте управляющий модуль, внесите его в список FeaturesModulesList первым. Теперь вы можете реализовать на нем IAppFeatureInitializer и ISessionFeatureInitializer чтобы создавать объекты логики, которые будут резолвить SessionAsyncEntryPoint / SсeneAsyncEntryPoint и вызывать на них StartAsync, либо просто в модуле биндить свои IAsyncEntryPoints как BindEntryPoint и исполнять уже в них.
 
 ## Добавляем фичи
 - создаем класс, добавляем на него интерфейс IFeatureModule и необходимые функциональные интерфейсы
 - в FeaturesModulesList добавляем новый модуль (используется SerializeReference)
 - Теперь по мере загрузки скоупов ресурсы будут загружаться, а зависимости - биндиться
 
 ## Функциональные интерфейсы модулей
 При сборке каждого скоупа логика будет проверять весь список модулей на наличие определенных функциональных интерфейсов и вызывать реализующие их объекты
 
 ### Scope-defined bindings
 - IAppFeatureScopeInstaller - зависимости будут внедряться в AppScope
 - ISessionFeatureScopeInstaller - в session scope
 - ISceneFeatureScopeInstaller - в scene scope (игровая сцена)
 
 ### Initializers
  IAppFeatureInitializer, ISessionFeatureInitializer и ISceneFeatureInitializer будут вызваны после сборки соответствующих контейнеров. 
  IAppFeaturePostInitializer, ISessionFeaturePostInitializer и ISceneFeaturePostInitializer будут вызваны во вторую очередь. 
  
 ### Resource loaders
 Тяжелые ресурсы асинхронно загружаются в 2 местах:
 - При сборке SessionScope (ISessionAsyncResourceLoader) - ресурсы самого приложения
 - При сборке SceneScope (ISceneAsyncResourceLoader) - ресурсы конкретной игровой сцены
 Перед AppScope ресурсы не грузятся асинхронно, app scope создается мгновенно. Загрузка использует возможность VContainer - асинхронные EntryPoints (RegisterEntryPoint + реализация Awaitable StartAsync(CancellationToken cancellation))
 Загруженные ресурсы оборачиваются в IResourceBinder и по завершению записываются в контейнер скоупа. Если их не нужно биндить, то просто возвращайте null в реализации метода загрузки.
 
 ### Добавление собственных функциональных интерфейсов
 - создайте функциональный интерфейс
 - добавьте на ваш модуль интерфейс инициализации (session/scope)
 - в реализации получите список модулей из резолвера
 - пройдитесь по списку и определите кто подходит
 
# Зависимости
 - TriInspector (только для проверки отсутствия дублирования в FeaturesModulesList, если вам это не нужно, добавьте NO_TRI_INSPECTOR в scripting define symbols)
 - VContainer 
 - ZE.Utils (используется AwaitablesExtension для параллельной загрузки ресурсов)
 <details>
<summary> git link</summary>
```bash
https://github.com/ZapilinEntertainment/Utils.git#main
```
</details>
 
 # Дополнительно
 - используйте SCOPES_LOG чтобы видеть сообщения о сборке скоупов
 - SceneScope может создаваться динамически, если вы не добавляете его в сцену. Добавляйте scene scope объектом чтобы внедрять зависимости в указанные в auto-inject объекты. Не забудьте снять галочку auto-run.
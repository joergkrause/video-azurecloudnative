using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heise.Course.WasmFrontend.Client.Services.Store
{

  public delegate void OnChangeDelegate(IState state);

  public class StoreService
  {

    private readonly IPersistanceService _persistanceService;

    public StoreService(IPersistanceService persistanceService, IServiceProvider provider)
    {
      _persistanceService = persistanceService;
      State.OnValueChanged += State_OnValueChanged;
      // Register reducers
      var reducers = GetType().Assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IReducer)));
      foreach (var reducer in reducers)
      {
        var ctor = reducer.GetConstructors().First();
        {
          var injectableServices = new List<object>();
          foreach (var param in ctor.GetParameters())
          {
            var injectableService = provider.GetService(param.ParameterType);
            if (injectableService != null)
            {
              injectableServices.Add(injectableService);
            }
          }
          Reducers.Add((IReducer)Activator.CreateInstance(reducer, injectableServices.ToArray())!);
        }
      }
    }
    private void State_OnValueChanged(object value)
    {
      if (OnChange != null)
      {
        OnChange(State);
      }
    }

    private IState State { get; set; } = new State();

    private IList<IReducer> Reducers { get; } = new List<IReducer>();

    public void Dispatch(IAction action)
    {
      // call all reducers and let them filter for action
      Parallel.ForEach<IReducer>(Reducers, async reducer => await reducer.InvokeAsync(State, action));
    }

    public event OnChangeDelegate? OnChange;

    public void PersistStore()
    {
      _persistanceService.Persist(State);
    }

    public void RestoreStore()
    {
      State = _persistanceService.Restore();
    }

    public async Task PersistStoreAsync()
    {
      await _persistanceService.PersistAsync(State);
    }

    public async Task RestoreStoreAsync()
    {
      State = await _persistanceService.RestoreAsync();
    }

  }
}

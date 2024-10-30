using Ecommerce.Presenters;

namespace Ecommerce;

public class ProgramManager
{
    private readonly MainPresenter _mainPresenter;

    public ProgramManager(MainPresenter mainPresenter)
    {
        _mainPresenter = mainPresenter;
    }

    public void Run()
    {
        _mainPresenter.MainMenu();
    }
}

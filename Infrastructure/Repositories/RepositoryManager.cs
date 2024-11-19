//using Application.Repositories;
//using Infrastructure.Data;

// This file is kept as a reference for later use of Unit of Work (UoW ) pattern
//namespace Infrastructure.newRepositories
//{
//    public class RepositoryManager : IRepositoryManager
//    {
//        private readonly AppDbContext _context;

//        private readonly IProductRepository _productRepository;
//        private readonly ICartRepository _cartRepository;
//        private readonly IAuthRepository _authRepository;
//        private readonly IOrderRepository _orderRepository;
//        public RepositoryManager(AppDbContext context)
//        {
//            _context = context;

//            _productRepository = new ProductRepository(context);
//            _cartRepository = new CartRepository(context);
//            _authRepository = new UserRepository(context);
//            _orderRepository = new OrderRepository(context);
//        }

//        public IProductRepository ProductRepository => _productRepository;
//        public ICartRepository CartRepository => _cartRepository;
//        public IAuthRepository AuthRepository => _authRepository;
//        public IOrderRepository OrderRepository => _orderRepository;
//        public void Save() => _context.SaveChanges();
//    }
//}

HU Edge .NET (Jan 2026)
Assessment Submission – Build, Debug & Defend
________________________________________
Candidate Overview
This document presents the end to end design, reasoning, and refactoring approach for a high scale Order, Payments, and Fulfilment platform using .NET, Microservices, CQRS, and Azure aligned architecture. The focus is on correctness, scalability, observability, and maintainability rather than full implementation code.
________________________________________
Assessment A – Build + Defend
1. Architecture Overview
1.1 System Goals
•	Handle burst traffic up to 10,000 orders per minute
•	Achieve exactly once effects as close as feasible
•	Maintain auditable workflows
•	Support independent scaling and failure isolation
1.2 Services & Boundaries
Service	Responsibility	Data Ownership
Orders Service	Order lifecycle & state	Orders DB
Payments Service	Payment authorization & capture	Payments DB
Fulfilment Service	Shipping & delivery lifecycle	Fulfilment DB
Catalog Read Service	Product & pricing lookup	NoSQL Read Store
API Gateway	Entry point, security, aggregation	N/A
Each service owns its data. No shared databases are allowed, preventing tight coupling and cross service corruption.
1.3 Communication
•	Synchronous (HTTP): Client → API Gateway → Services
•	Asynchronous (Events): Orders → Payments → Fulfilment
Failures are isolated and retried asynchronously without blocking the user.
________________________________________
2. CQRS Artifacts
2.1 Commands (Write Side – EF Core)
•	CreateOrder
•	AddOrderItem
•	ConfirmPayment
•	FailPayment
•	StartFulfilment
•	CompleteFulfilment
Commands are idempotent and validated before execution.
2.2 Queries (Read Side – Dapper / NoSQL)
•	GetOrderSummary(orderId)
•	GetCustomerOrders(customerId)
•	GetPaymentStatus(orderId)
•	GetFulfilmentTracking(orderId)
2.3 Read Models
Read Model	Storage
OrderSummaryView	SQL (Dapper)
CustomerOrderHistory	NoSQL (Cosmos style)
FulfilmentStatusView	SQL (Dapper)
________________________________________
3. Consistency & Exactly Once Strategy
3.1 Idempotency
•	Each command carries an IdempotencyKey
•	Stored and validated using Redis or SQL
•	Duplicate requests are ignored safely
3.2 Retries & Deduplication
•	Exponential backoff for transient failures
•	Message consumers track processed MessageIds
•	Duplicate messages are skipped
3.3 Ordering
•	Messages are partitioned by OrderId
•	Guarantees sequential processing per aggregate
3.4 Outbox Pattern
•	Domain events stored in the same transaction as state changes
•	Background publisher ensures reliable event delivery
________________________________________
4. Data Access Strategy
4.1 EF Core – Write Model
•	Aggregate Root: Order
•	Single transaction per aggregate
•	Optimistic concurrency using RowVersion
•	Tracking enabled only for writes
Rationale: EF Core enforces business invariants and transactional consistency.
4.2 Dapper – Read & Reporting
•	Flat DTO projections
•	SQL level filtering & pagination
•	OFFSET/FETCH paging
•	Zero tracking overhead
Rationale: Dapper provides predictable SQL and superior read performance.
4.3 EF Core vs Dapper – Decision Rubric
Use Case	Tool
Domain rules & consistency	EF Core
High volume reads	Dapper
Reporting	Dapper
Complex transactions	EF Core
________________________________________
5. API Gateway Specification
5.1 Routes
•	POST /orders
•	GET /orders/{id}
•	POST /payments/{orderId}
•	GET /order-details/{id} (aggregated view)
5.2 Policies
•	JWT Authentication
•	Role based Authorization
•	Rate limiting per user/IP
•	Correlation ID injection
•	Response aggregation
________________________________________
6. LINQ – Performance Safe Usage
6.1 Grouping & Projection
•	GroupBy with server side translation
6.2 Conditional Projection
•	Null safe projections avoiding client evaluation
6.3 Pagination
•	OrderBy + Skip + Take
•	Translates directly to SQL OFFSET/FETCH
All queries are validated to avoid N+1 issues and client side execution.
________________________________________
7. Memory & Performance Management
7.1 Common Memory Leak Vectors
1.	Unbounded in memory caches
2.	EF Core tracking misuse
3.	Event handlers not unsubscribed
4.	Large object graphs
5.	Incorrect DI lifetimes
7.2 Investigation Tools
•	dotnet counters
•	dotnet dump
•	PerfView
7.3 Mitigations
•	Distributed cache (Redis)
•	AsNoTracking for reads
•	Proper service lifetimes
•	Bounded cache sizes
________________________________________
8. Azure Conceptual Mapping
Workload	Azure Service
APIs	Azure App Service
Background processing	Azure Functions
Orchestration	Logic Apps
Cache & Idempotency	Azure Redis Cache
Documents & invoices	Blob Storage
Write database	Azure SQL Database
Read models	Cosmos DB
________________________________________
Assessment B – Debug + Refactor
Identified Issues
•	Monolithic service violating SOLID
•	EF Core used for heavy reads
•	Naive in memory cache causing memory growth
•	LINQ N+1 queries
•	Missing gateway policies
Refactor Plan
1.	Split into Orders, Payments, Fulfilment services
2.	Writes via EF Core, reads via Dapper
3.	Replace in memory cache with Redis
4.	Fix LINQ queries using joins and projections
5.	Introduce API Gateway policies
Memory Leak Root Cause & Fix
•	Static cache without eviction
•	EF tracking large datasets
Fix: Distributed cache, AsNoTracking, proper disposal
Non Relational Read Model (Example)
•	Customer centric order history document
________________________________________


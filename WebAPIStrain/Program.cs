using Microsoft.EntityFrameworkCore;
using WebAPIStrain.Services;
using WebAPIStrain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPIStrain.Models;
using WebAPIStrain.PaymentServices.Momo.Config;
using System.Configuration;
using WebAPIStrain.PaymentServices.VNPay;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddTransient<Seed>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IrtContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//bỏ chặn CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

//các Repository Pattern 
builder.Services.AddScoped<IPhylumRepository, PhylumRepository>();
builder.Services.AddScoped<IStrainRepository, StrainRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IGenusRepository, GenusRepository>();
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddScoped<IRoleForEmployeeRepository, RoleForEmployeeRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartDetailRepository, CartDetailRepository>();
builder.Services.AddScoped<IConditionRepository, ConditionRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IStrainApprovalHistoryRepository, StrainApprovalHistoryRepository>();
//quận huyện
builder.Services.AddScoped<IWardsRepository, WardsRepository>();
builder.Services.AddScoped<IDistrictsRepository, DistrictsRepository>();
builder.Services.AddScoped<IProvincesRepository, ProvincesRepository>();
//Tanh thêm
builder.Services.AddScoped<IIsolatorStrainRepository, IsolatorStrainRepository>();
builder.Services.AddScoped<IIdentifyStrainRepository, IdentifyStrainRepository>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectContentRepository, ProjectContentRepository>();
builder.Services.AddScoped<IContentWorkRepository, ContentWorkRepository>();
builder.Services.AddScoped<IAccountForCustomerRepository, AccountForCustomerRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillDetailRepository, BillDetailRepository>();
builder.Services.AddScoped<IScienceNewspaperRepository, ScienceNewspaperRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

//send mail
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailServiceRepository, MailServiceRepository>();
builder.Services.AddMemoryCache();
//Momo payment
builder.Services.Configure<MomoConfig>(builder.Configuration.GetSection("Momo"));
builder.Services.AddHttpClient<IMomoService, MomoService>();
    //builder.Services.AddHttpClient();

//VPPay
builder.Services.Configure<VNPayConfig>(builder.Configuration.GetSection("VNPay"));
builder.Services.AddSingleton<IVNPayService, VNPayService>();
builder.Services.AddHttpContextAccessor();




//token
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secrectKey = builder.Configuration.GetSection("AppSettings")["SecretKey"];
var secrectKeyBytes = Encoding.UTF8.GetBytes(secrectKey);

builder.Services.AddAuthentication
    (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(otp =>
    {
        otp.TokenValidationParameters = new TokenValidationParameters
        {
            //tự cấp token
            ValidateIssuer = false,
            ValidateAudience = false,
            //ký vào token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secrectKeyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(); 

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

# Installment Ledger — Admin Panel

A store-owner admin panel for an installment-finance / "buy now, pay later" business,
built as an ASP.NET Core MVC (.cshtml) project.

## What's included

- **Views/Shared/_Layout.cshtml** — sidebar shell used by every page
- **wwwroot/css/admin.css** — full design system (ledger/paper aesthetic, no Bootstrap defaults)
- **Dashboard** — income, pending dues, overdue amount, due-this-week, recent activity
- **Customers** — list + create, with 1–2 guarantors per customer (required by domain)
- **Vendors** — suppliers you buy stock from
- **Products** — cash price vs installment price, stock, vendor link
- **Installment Plans** — reusable templates (duration, down payment %, markup %, frequency)
- **Sales** — create a sale by combining customer + product + plan, auto schedule
- **Payments (Collections)** — due/overdue list, record-payment modal, late fee display
- **Reports** — income breakdown by vendor, defaulters list, exportable
- **Settings** — store profile, currency, **configurable late fee rules** (fixed/percentage,
  grace period, recurrence), auto-default flagging, notification toggles
- **Admin Users & Roles** — owner can invite staff (Manager / Sales Staff / Collections Staff)
- **Account/Login** — sign-in page (auth wiring left as TODO, see below)

## Wiring this to a real backend

Everything currently runs on in-memory sample lists inside each controller so you can
run the project immediately and see real screens. To make it production-ready:

1. **Add EF Core + a DbContext** modeled on `Models/Models.cs` (Customer, Guarantor,
   Vendor, Product, InstallmentPlan, Sale, InstallmentDue, AdminUser, StoreSettings).
   Replace the `static List<T>` fields in each controller with DbContext queries.
2. **Add authentication** — cookie auth is stubbed in `Program.cs` and `AccountController`.
   Hash passwords (e.g. `PasswordHasher<T>`), sign in with claims including `Role`,
   and protect controllers with `[Authorize(Roles = "...")]` (TODO comments mark where).
3. **Installment schedule generation** — when a Sale is created, generate N
   `InstallmentDue` rows from `InstallmentPlan.DurationMonths` /
   `PaymentFrequency`, splitting `(ProductPrice + Markup - DownPayment)` evenly across them.
4. **Late fee engine** — a background job (e.g. hosted service or scheduled task) should
   scan `InstallmentDue` rows past `DueDate + GracePeriodDays` and apply
   `StoreSettings.LateFeeType/LateFeeValue/LateFeeRecurrence`, matching what's configured
   under Settings → Late Fee Rules.
5. **Multi-tenancy** — since this is "shipped to a shop," each store owner's data should
   be scoped by a `StoreId`, set at signup/provisioning.

## Design notes

The visual language is a "ledger/paper" aesthetic rather than a generic admin-dashboard
template: warm paper background, ink-navy sidebar, brass accent, and a signature
rotated "stamp" badge (`.stamp-paid`, `.stamp-due`, `.stamp-overdue`) used for payment
status everywhere, evoking a rubber ink stamp on a physical ledger book. Money values
use a monospace font (IBM Plex Mono) with tabular figures for scannable columns.

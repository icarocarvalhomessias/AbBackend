﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; }

    /// <summary>
    /// Gets or sets the date of the sale.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the user ID associated with the sale.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Gets or sets the total discount applied to the sale.
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    private readonly List<SaleItem> _items;

    /// <summary>
    /// Gets the collection of sale items.
    /// </summary>
    public IReadOnlyCollection<SaleItem> Items => _items;

    public User _user;

    public Sale()
    {
        _items = new List<SaleItem>();

    }

    /// <summary>
    /// Initializes a new instance of the Sale class with the specified user ID and branch.
    /// </summary>
    /// <param name="userId">The user ID associated with the sale.</param>
    /// <param name="branch">The branch where the sale occurred.</param>
    public Sale(Guid userId, string branch)
    {
        SaleNumber = Guid.NewGuid().ToString();
        SaleDate = DateTime.Now;
        UserId = userId;
        Branch = branch;
        _items = new List<SaleItem>();
    }

    /// <summary>
    /// Adds an item to the sale.
    /// </summary>
    /// <param name="item">The sale item to add.</param>
    public void AddItem(SaleItem item)
    {
        if (!item.Validate().IsValid) throw new DomainException("Sale Item is not valid.");

        if (SaleItemExists(item))
        {
            var existingItem = _items.First(si => si.ProductId == item.ProductId);
            existingItem.ChangeQuantity(item.Quantity);
            item = existingItem;

            _items.Remove(existingItem);
        }

        _items.Add(item);

        CalculateSale();
    }

    /// <summary>
    /// Removes an item from the sale.
    /// </summary>
    /// <param name="item">The sale item to remove.</param>
    public void RemoveItem(SaleItem item)
    {
        if (!item.Validate().IsValid) throw new DomainException("Sale Item is not valid.");

        var existingItem = _items.First(si => si.ProductId == item.ProductId);

        if (existingItem is null) throw new DomainException("Sale Item not found.");

        _items.Remove(existingItem);

        CalculateSale();
    }

    /// <summary>
    /// Updates an item in the sale.
    /// </summary>
    /// <param name="item">The sale item to update.</param>
    public void UpdateItem(SaleItem item)
    {
        if (!item.Validate().IsValid) throw new DomainException("Sale Item is not valid.");

        var existingItem = _items.First(si => si.ProductId == item.ProductId);

        if (existingItem is null) throw new DomainException("Sale Item not found.");

        _items.Remove(existingItem);
        _items.Add(item);

        CalculateSale();
    }

    /// <summary>
    /// Updates the quantities of an item in the sale.
    /// </summary>
    /// <param name="item">The sale item to update.</param>
    /// <param name="quantities">The new quantity of the product in the sale item.</param>
    public void UpdateQuantities(SaleItem item, int quantities)
    {
        item.ChangeQuantity(quantities);

        UpdateItem(item);
    }

    /// <summary>
    /// Checks if a sale item exists in the sale.
    /// </summary>
    /// <param name="item">The sale item to check.</param>
    /// <returns>True if the sale item exists, otherwise false.</returns>
    public bool SaleItemExists(SaleItem item)
    {
        return _items.Any(si => si.ProductId == item.ProductId);
    }

    /// <summary>
    /// Cancels the sale.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Calculates the total amount of the sale.
    /// </summary>
    public void CalculateSale()
    {
        TotalAmount = _items.Sum(si => CalculateTotalAmountDiscount(si));
    }

    /// <summary>
    /// Calculates the discount for a sale item.
    /// </summary>
    /// <param name="saleItem">The sale item to calculate the discount for.</param>
    /// <returns>The discounted amount for the sale item.</returns>
    public decimal CalculateTotalAmountDiscount(SaleItem saleItem)
    {
        var discount = GetDiscount(saleItem.Quantity);

        return saleItem.Quantity * saleItem.UnitPrice * (1 - discount);
    }

    /// <summary>
    /// Gets the discount percentage based on the quantity of the sale item.
    /// </summary>
    /// <param name="quantity">The quantity of the sale item.</param>
    /// <returns>The discount percentage.</returns>
    public decimal GetDiscount(int quantity)
    {
        return quantity switch
        {
            >= 4 and < 10 => 0.10m,
            >= 10 and <= 20 => 0.20m,
            > 20 => throw new DomainException("Quantity cannot be greater than 20."),
            _ => 0m
        };
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

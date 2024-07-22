using System;

public interface IBossController
{
	event Action OnHealthChanged;
	int currentHealth { get; }
	int maxHealth { get; }
	void TakeDamage(int damage);
}